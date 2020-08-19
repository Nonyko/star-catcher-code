using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevelOnFallingController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void  OnTriggerEnter2D(Collider2D col){
        
        if(col.CompareTag("Player")){
            GameObject Player = GameObject.Find("Player");
           //reset level
           Player.GetComponent<HealthController>().health = 0;
        }
     }
}
