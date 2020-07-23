using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class teleportController : MonoBehaviour
{
    public UnityEvent LevelCompletedEvent;
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
            LevelCompletedEvent.Invoke();
           }
    }
}
