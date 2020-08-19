using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleAnimalController : MonoBehaviour
{
    [SerializeField] private LayerMask WhatToLook;
    public Transform VisionPoint;
    public    const float VisionRadius = 8f;

     public    const float FlyRadius = 2f;

    Animator animator;
    bool isSafe = true;

    string state = "Safe";
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void FixedUpdate() {        
         AlertArea();

         Collider2D[] colliders = Physics2D.OverlapCircleAll(VisionPoint.position, FlyRadius, WhatToLook);
         for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
             
                  //PlayerAvistado
                  
                   // animator.SetTrigger("Alert");
                    state = "Fly";
                  
            }
        }

        if(state.Equals("Fly")){
             float newY = gameObject.transform.position.y + 5 * Time.fixedDeltaTime;
             gameObject.transform.position = new Vector3(gameObject.transform.position.x, newY, gameObject.transform.position.z);
             animator.SetTrigger("Leaving");
        }


      }


      void AlertArea(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(VisionPoint.position, VisionRadius, WhatToLook);
         for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
             
                  //PlayerAvistado
                  if(!state.Equals("Fly")) {    
                    animator.SetTrigger("Alert");
                    state = "Alert";
                  }
            }
        }
        if(colliders.Length <=0){
          //Player saiu da area de alerta       
          if(!state.Equals("Fly")) {           
              animator.SetTrigger("Safe");  
               state = "Safe";
          }
               
        }
      }
}
