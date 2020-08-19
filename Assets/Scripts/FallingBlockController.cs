using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockController : MonoBehaviour
{
   
    float timeToFall = 1f;
    bool collisionWithPlayer = false;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToFall<= 0f){
            animator.SetBool("IsFalling", true);
            Destroy(gameObject, 0.5f);
        }
    }

     void OnCollisionEnter2D(Collision2D collision){        
        if(collision.gameObject.CompareTag("Player")){
            collisionWithPlayer = true;
        }
    }

     void  OnTriggerEnter2D(Collider2D col){
         if(col.CompareTag("Bullet")){
             collisionWithPlayer = true;
            }
    }

    void FixedUpdate(){
        if(collisionWithPlayer){
            timeToFall -= 1 * Time.fixedDeltaTime;
        }
        
    }
}
