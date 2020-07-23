using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    
    
    public  float speed = 20f;
    public Rigidbody2D rb;

    public ParticleSystem bulletParticles;

   

    // Start is called before the first frame update
    void Start()
    {   
        bulletParticles =  gameObject.transform.GetChild (0).gameObject.GetComponent<ParticleSystem>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 0.5f);
    }

     void  OnTriggerEnter2D(Collider2D col){
         if(col.CompareTag("Enemy") || col.CompareTag("Floor") || col.CompareTag("FallingBlock")){
            //health--;
           shotVisuals();
           Destroy(gameObject, 0.2f);
        }
     }
    
    void shotVisuals(){
            gameObject.GetComponent<SpriteRenderer>().color =  new Color(0f, 0f, 0f, 0f);
           gameObject.GetComponent<BoxCollider2D>().enabled = false;
           bulletParticles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
