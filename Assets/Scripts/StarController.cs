using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
public class StarController : MonoBehaviour
{
    public UnityEvent OnStarCollected;
    public AudioSource StarSound;

    public List<GameObject> starsToDestroy = new List<GameObject>();

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

     void OnTriggerEnter2D(Collider2D col)
    {            
        if(col.gameObject.tag == "Player"){
            StarSound.Play();           
            if(!starsToDestroy.Contains(gameObject)){
                animator.SetBool("IsStarCollected", true);
                starsToDestroy.Add(gameObject);
            }
            //Destroy(gameObject,0.5f);
        }
        
    }

    IEnumerator  DestruirEstrelas(){    
        yield return 1f;    
        if(starsToDestroy.Count>0){               
            foreach (var star in starsToDestroy)
            {                
                OnStarCollected.Invoke();
                star.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(star, 0.5f);
            }
            starsToDestroy = new List<GameObject>();
        }        
    }
    // Update is called once per frame
    
    
    void Update()
    {
        StartCoroutine(DestruirEstrelas());
    }
}
