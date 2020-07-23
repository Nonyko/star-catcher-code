using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public AudioSource DamageSound;

    
    public List<GameObject> damageToTake = new List<GameObject>();

  
     [System.Serializable]
	public class FloatEvent : UnityEvent<float> { }

    
	public FloatEvent OnDamageEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         StartCoroutine(TakeDamage());

        for(int i = 0; i<hearts.Length; i++){

            if(i<health){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }

            if(i < numOfHearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = true;
            }
        }
        
    }

    void FixedUpdate(){
                timeBeforeResetInvunerable -= 1 * Time.fixedDeltaTime;

    }
    float invunerableTimeRemember = 0.5f;
    float timeBeforeResetInvunerable = 0;
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Enemy") && timeBeforeResetInvunerable <= 0){
            //health--;
           
            if(!damageToTake.Contains(gameObject)){
                //animator.SetBool("IsStarCollected", true);
                damageToTake.Add(gameObject);
            }
        }
    }
     void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Enemy") && timeBeforeResetInvunerable <= 0){
            //health--;
           
            if(!damageToTake.Contains(gameObject)){
                //animator.SetBool("IsStarCollected", true);
                damageToTake.Add(gameObject);
            }
        }
    }
    IEnumerator  TakeDamage(){   
        
        if(damageToTake.Count>0){               
            foreach (var enemy in damageToTake)
            {                
                OnDamageEvent.Invoke(3f);
                DamageSound.Play();
                health--;
                timeBeforeResetInvunerable = invunerableTimeRemember;
            }
            damageToTake = new List<GameObject>();
        } 
        yield return new WaitForSeconds(0.05f);       
    }

}
