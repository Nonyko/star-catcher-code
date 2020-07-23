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

    [System.Serializable]
	public class FloatEvent : UnityEvent<float> { }
  
	public FloatEvent OnShootEvent;
    // Update is called once per frame
    void Awake(){
        
        if (OnShootEvent == null)
			OnShootEvent = new FloatEvent();
    }
    void Update()
    {
        if(!isDead){
            horizontalMove =Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (Input.GetButtonDown("Jump")){
            jumpPressedTimeBeforeReset = jumpPressedRememberTime;
        }

        if (jumpPressedTimeBeforeReset > 0){
            jump = true;
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
         animator.SetBool("IsJumping", false);        
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
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch , jump);      
        jump = false;
        timeBeforeResetShoot +=1  * Time.fixedDeltaTime;
        jumpPressedTimeBeforeReset -= 1  * Time.fixedDeltaTime;
         if(timeBeforeResetShoot>0.05){
            timeBeforeResetShoot=0;
           animator.SetBool("IsShooting", false);
        }       
        
    }

  
}
