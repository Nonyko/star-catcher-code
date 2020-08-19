using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlantController : MonoBehaviour
{
    public  float speed = 5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {        
        rb.velocity = -transform.right * speed;
       Destroy(gameObject, 3f);
    }
    void  OnTriggerEnter2D(Collider2D col){
         if(col.CompareTag("Floor")){
             Destroy(gameObject);
         }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
