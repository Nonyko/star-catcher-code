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

    public bool isDead = false;
    public bool IsEntering = true;

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

    //checa se horizontal axis raw mudou para zero
    float OldHorizontalMove = 0;
    bool ChangedToZero = false;

    bool ChangedToOne = false;

    bool HorizontalMovementChangedToOne(float horizontalMove){
        float NewHorizontalMove = horizontalMove;
        bool changedToOne = false;
        if(OldHorizontalMove!=1 && OldHorizontalMove!=-1){
            if(NewHorizontalMove==1 || NewHorizontalMove==-1){
               //mudou para 0
            //    Debug.Log("MUDOU PRA 1 OU -1");
                OldHorizontalMove = NewHorizontalMove;
                changedToOne =  true;
                // StartCoroutine(ChangedToOneCoroutine());
                ChangedToOne = true;
                //  Debug.Log(changedToOne);
            }
        }
        OldHorizontalMove = NewHorizontalMove;
        return changedToOne;
    }
 
  
    void Update()
    {
        if(IsEntering){
            StartCoroutine(FinishEntering());
        }
        if(!isDead && !IsEntering){
            horizontalMove =Input.GetAxisRaw("Horizontal") * runSpeed;
        
           
            HorizontalMovementChangedToOne(Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
            
            //dash: se button horizontal true: iniciar countdown, se antes de chegar a zero for true novamente,
            //e os dois comandos rapidos forem na mesma direcao executar dash 
            //Debug.Log("retornou "+);
            if(Input.GetButtonDown("Horizontal") || ChangedToOne){
              
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
               
               ChangedToOne = false;
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
        
        if (Input.GetButtonDown("Crouch") || Input.GetAxisRaw("Vertical")==-1){
            crouch = true;           
        } else if(Input.GetButtonUp("Crouch")  || Input.GetAxisRaw("Vertical")!=-1){
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
        OnShootEvent.Invoke(10f);
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
