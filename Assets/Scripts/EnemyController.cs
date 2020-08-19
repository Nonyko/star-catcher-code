using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{   
    private Dictionary<string, int> movingTO = new Dictionary<string, int>();
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    [SerializeField] private float m_JumpForce = 4000f;							// Amount of force added when the player jumps.
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true; 
    public float runSpeed = 40f;
    private float horizontalMove = 0;
    private float move;
    private bool jump=false;
    public bool canJump = false;
    private bool moveToggle = false;
    public float waitTimeBeforeJump = 0.5f;
    public string movementString = "left";
    int moveTo = 0;
    public int changeDirectionCounterInitial = 100;
    public bool dontPassInvisibleWallBehavior = true;
    private int changeDirectionCounter = 0;

    public string movementBehaviorString = "random";
    public Animator animator;

    public int EnemyLife = 1;

    public ParticleSystem EnemyExplosionParticles;
    public AudioSource EnemyDestructionSound;

    public GameObject explosionPrefab;

    [System.Serializable]
	public class FloatEvent : UnityEvent<float> { }
    [Header("Events")]
	[Space]

	public FloatEvent OnHitedEvent;


    	private void Awake()
	{
         
        changeDirectionCounter = changeDirectionCounterInitial;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        movingTO.Add("right", 1);
        movingTO.Add("left", -1);
        movingTO.Add("none", 0);

        if (OnHitedEvent == null)
			OnHitedEvent = new FloatEvent();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        movementBehavior(movementBehaviorString);
        //mudar de lado quando bater em edge
    }

     void FixedUpdate(){
            
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded){				
				//OnLandEvent.Invoke();
                if(canJump){
                    StartCoroutine(ExecuteJump());     
                }
                           
				}
					
			}
		}

          Move(horizontalMove  * Time.fixedDeltaTime, jump );   
         jump = false;

         //Invunerable countdown
         if(Invunerable){
             TimeBeforeLoseInvunerable -=1  * Time.fixedDeltaTime;
             if(TimeBeforeLoseInvunerable<=0){
                 Invunerable = false;
             }
         }

     }
   

      IEnumerator  ExecuteJump(){
          yield return waitTimeBeforeJump;    
          jump = true;
      }


    public void Move(float move, bool jump)
	{
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
       if(m_Grounded && jump){        
         m_Grounded = false;
        // little jump
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
       }
       

        // If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}    

    }

    private void Flip()
	{		
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f,0f);
	}

    private int alwaysMoveTo(string movementString){
        return movingTO[movementString];
    }
    
    private int randomMovimento(){        
        changeDirectionCounter--;
        if(changeDirectionCounter<=0){
            moveTo =  Random.Range(-1,2);
            changeDirectionCounter = changeDirectionCounterInitial;
        }
        return moveTo;
    }

    //fixed or random
    bool finishedToCommandMoveFlag = false;
    void movementBehavior(string behavior){
        //Control if movement needs to change to the other side.
        if(!moveToggle){
            switch(behavior){
                case "fixed":
                horizontalMove =  alwaysMoveTo(movementString);
                break;
                case "random":
                horizontalMove = randomMovimento();
                break;
                case "none":
                horizontalMove =  0;
                break;
            }
            horizontalMove = horizontalMove * runSpeed;
        }
        
        if(moveToggle){
            if(!finishedToCommandMoveFlag){
                 horizontalMove = horizontalMove * -1;
                 if(movementString=="right"){
                     movementString = "left";
                 }else{
                    movementString = "right";
                 }
                 //Debug.Log(horizontalMove);
                 finishedToCommandMoveFlag = true;
            }
            
        }

    }
    float TimeBeforeLoseInvunerableRemember = 0.4f;
    float TimeBeforeLoseInvunerable = 0;
    bool Invunerable = false;


    void  OnTriggerEnter2D(Collider2D col){
        
        if(col.CompareTag("InvisibleWall")){
            
            if(dontPassInvisibleWallBehavior){
                //horizontalMove = horizontalMove * -1;
                moveToggle = true;
                finishedToCommandMoveFlag = false;
            }
        }
        
        if(col.CompareTag("Bullet")){       
          
           animator.SetBool("IsTakingDamage",true);
           OnHitedEvent.Invoke(10f);

           if(!Invunerable){
                EnemyLife = EnemyLife - 1;
                TimeBeforeLoseInvunerable = TimeBeforeLoseInvunerableRemember;
                Invunerable = true;
           }
          
             
           if(EnemyLife<=0){
             
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                EnemyExplosionParticles.Play();
                EnemyDestructionSound.Play();
                Instantiate(explosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject, 0.3f);
           }
           StartCoroutine(ReturnToDefaultAnimationAfterHited());
        }

    }

    void  OnTriggerExit2D(Collider2D col){
        if(col.CompareTag("InvisibleWall")){
            if(dontPassInvisibleWallBehavior){                
                StartCoroutine(ReturnToDefaultBehavior());
            }
        }
    }

    // void OnColliderEnter2D(Collision2d other){
    //     if(other.CompareTag("InvisibleWall")){}
    // }

    IEnumerator  ReturnToDefaultBehavior(){
        yield return new WaitForSeconds(1);
         moveToggle = false;
    }

    IEnumerator  ReturnToDefaultAnimationAfterHited(){
        yield return new WaitForSeconds(0.35f);
         animator.SetBool("IsTakingDamage",false);
    }

}
