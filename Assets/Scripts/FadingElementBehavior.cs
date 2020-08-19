using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingElementBehavior : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

       void  OnTriggerEnter2D(Collider2D col){
           if(col.CompareTag("Player") ){    
               
               //fade in
               animator.SetBool("PlayerIsBehind", true);
           }
      }

       void  OnTriggerExit2D(Collider2D col){
           if(col.CompareTag("Player") ){    
               
               //fade out
              animator.SetBool("PlayerIsBehind", false);
           }
      }
}
