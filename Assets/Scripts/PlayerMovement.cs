using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public Animator animator;

    float timeBeforeResetShoot = 0f;

    float jumpPressedRememberTime = 0.2f;
    float jumpPressedTimeBeforeReset  = 0;
    int starsCollected;

    bool isDead = false;
    bool IsEntering = true;

    bool doubleJump = false;
    bool hadDoubleJumped = false;
    bool onAir = false;

    
    
    [System.Serializable]
	public class FloatEvent : UnityEvent<float> { }
  
	public FloatEvent OnShootEvent;

    public UnityEvent OnFinishEntering;
    // Update is called once per frame
    void Awake(){
        
        if (OnShootEvent == null)
			OnShootEvent = new FloatEvent();

        if (OnFinishEntering == null)
			OnFinishEntering = new UnityEvent();    
    }
    float DashCountDown =0f;
    float DashCountDownRemember = 0.2f;
    bool IsDashing = false;
    float oldDirection = 0;
    bool dash = false;
    void Update()
    {
        if(IsEntering){
            StartCoroutine(FinishEntering());
        }
        if(!isDead && !IsEntering){
            horizontalMove =Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            //dash: se button horizontal true: iniciar countdown, se antes de chegar a zero for true novamente,
            //e os dois comandos rapidos forem na mesma direcao executar dash 
            
            if(Input.GetButtonDown("Horizontal")){
              
                //quando eu aperto horizontal,
                // se eu ja nao estiver dando dash,
                // inicio um contador de tempo para o comando de dash.
                 if(DashCountDown>0){
                  DashCountDown = 0;
                  IsDashing =  true;
                
                  if(oldDirection == Input.GetAxisRaw("Horizontal")){
                        // Debug.Log("executar dash");
                        dash = true;
                  }
                }

                if(!IsDashing){
                   DashCountDown = DashCountDownRemember;
                   oldDirection = Input.GetAxisRaw("Horizontal");
                }
               
               
            }

            

        if (Input.GetButtonDown("Jump")){
            jumpPressedTimeBeforeReset = jumpPressedRememberTime;
            
           if(onAir && !doubleJump ){
               if(!hadDoubleJumped){
                doubleJump = true;
               	// Debug.Log("Executar double jump");
                   hadDoubleJumped = true;
                    animator.SetBool("IsDoubleJumping", true);
                }
           }
        }

        if (jumpPressedTimeBeforeReset > 0){
            jump = true;
            onAir = true;
            animator.SetBool("IsJumping", true);
        }
        
        if (Input.GetButtonDown("Crouch")){
            crouch = true;           
        } else if(Input.GetButtonUp("Crouch")){
            crouch = false;           
        }
        }
        

    }
    
    public void OnLanding(){
        onAir = false;
        hadDoubleJumped = false;
        animator.SetBool("IsJumping", false); 
        animator.SetBool("IsDoubleJumping", false);
          
    }

    public void OnCrouching(bool isCrouching){
     animator.SetBool("IsCrouching", isCrouching);
    }
    public void OnShoot(){
        timeBeforeResetShoot=0f;
        OnShootEvent.Invoke(5f);
        animator.SetBool("IsShooting", true); 
        
    }
    public void OnStarCollected(){
         starsCollected += 1;
    }

   

    IEnumerator FinishEntering(){
         yield return new WaitForSeconds(2f);
         // trigger the stop animation events here
          IsEntering = false;
          OnFinishEntering.Invoke();
     }
    public void OnTakeDamage(){
        animator.SetBool("IsTakingDamage", true);
        if(gameObject.GetComponent<HealthController>().health==0){
            isDead = true;
        }
        StartCoroutine(ResetDamageAnimation());
    }

    IEnumerator  ResetDamageAnimation(){ 
        yield return new WaitForSeconds(0.08f);  
         animator.SetBool("IsTakingDamage", false);   
      }

    void FixedUpdate(){
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch , jump, doubleJump, dash);  
        IsDashing =  false;    
        jump = false;
        dash = false;
        doubleJump = false;
        timeBeforeResetShoot +=1  * Time.fixedDeltaTime;
        jumpPressedTimeBeforeReset -= 1  * Time.fixedDeltaTime;
         if(timeBeforeResetShoot>0.05){
            timeBeforeResetShoot=0;
           animator.SetBool("IsShooting", false);
        }    

        if(DashCountDown>0){
            DashCountDown  -= 1  * Time.fixedDeltaTime;
        }
       
    }

  
}
