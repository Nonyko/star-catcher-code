using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyBehaviorContoller : MonoBehaviour
{
    bool IsAgressive = false;
    bool HasStartedToFollow = false;
    [SerializeField] private LayerMask WhatToLook;
    public Transform VisionPoint;
    public    const float VisionRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void FixedUpdate() {        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(VisionPoint.position, VisionRadius, WhatToLook);
        // Debug.Log(colliders.Length);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
             
                   IsAgressive = true;
                  //"Player avistado"
                   gameObject.GetComponent<Animator>().SetBool("IsChasingPlayer", true);
                    StartCoroutine(ChasePlayer(colliders[i].gameObject));
           
            }
        }
        if(colliders.Length <=0){
            if(IsAgressive){
                IsAgressive = false;
                    StopChasing();
            }
        }
    }
    IEnumerator ChasePlayer(GameObject Player){
       
        // saber que direçao tenho que ir
        //mudar behavior de movimento de none para fixed
        //setar direção que precisa ir
        yield return new WaitForSeconds(1f);    
        
        float xMonster = gameObject.transform.position.x;
        float xPlayer =   Player.transform.position.x;
        
        if(xPlayer < xMonster){
            //"Player a esquerda
            gameObject.GetComponent<EnemyController>().movementString = "left";
        }else{
             //"Player a direita"
               gameObject.GetComponent<EnemyController>().movementString = "right";
        }

        gameObject.GetComponent<EnemyController>().movementBehaviorString="fixed";
    }

    void StopChasing(){
         gameObject.GetComponent<Animator>().SetBool("IsChasingPlayer", false);
         gameObject.GetComponent<EnemyController>().movementBehaviorString="none";
    }

    //   void  OnTriggerEnter2D(Collider2D col){
    //        if(col.CompareTag("Player") ){    
               
    //            if(!IsAgressive){
    //                IsAgressive = true;
    //                Debug.Log("Player avistado");
    //                gameObject.GetComponent<Animator>().SetBool("IsChasingPlayer", true);
    //                 StartCoroutine(ChasePlayer(col.gameObject));
    //            }
               
    //        }
    //   }

    //     void  OnTriggerExit2D(Collider2D col){
    //        if(col.CompareTag("Player")){
              
    //              if(IsAgressive){
    //                IsAgressive = false;
    //                 Debug.Log("Fora de vista");
    //                 StopChasing();
    //            }
    //        }
    //   }

 
}
