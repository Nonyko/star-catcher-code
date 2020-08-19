using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlantShoot : MonoBehaviour
{   
    public Transform FirePoint;
    public GameObject bulletPrefab;

   public AudioSource ShootSound;

   public UnityEvent OnPlantShootEvent;

     Animator animator;

     public int[] ShootSequence = {1}; // 1 to up, 0 to down
     int nextShootDirection;
     int shootIndex = 0;

     float timeBeforeChangeShootDirection = 0f;
    // Start is called before the first frame update
    void Start()
    {
       animator = gameObject.GetComponent<Animator>();
       nextShootDirection = ShootSequence[0];
    }

    // Update is called once per frame
    void Update()
    {     
       ShootBehavior();
       if(timeBeforeChangeShootDirection<=0){
           ChangeShootDirection();
           timeBeforeChangeShootDirection = 0.5f;
       }
    }
    void FixedUpdate(){
      timeBeforeChangeShootDirection = timeBeforeChangeShootDirection - 1 * Time.fixedDeltaTime;
    }
    void Shoot(){
        OnPlantShootEvent.Invoke();  
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        //ShootSound.Play();
    }

    //Shoot behaviors: random, sequence
    void ShootBehavior(){
                 
          if(nextShootDirection==0){
                animator.SetBool("ShootDown",true);
            }else{
                 animator.SetBool("ShootDown",false);
            }
      
    }
    void ChangeShootDirection(){
         if(shootIndex+1 < ShootSequence.Length){
             shootIndex++;           
         }else{
             shootIndex = 0;
         }
         nextShootDirection =  ShootSequence[shootIndex];        
    }
    // IEnumerator ChangeShootDirectionAndWait(int shootDirection){
    //      if(shootIndex+1 < ShootSequence.Length){
    //          shootIndex++;           
    //      }else{
    //          shootIndex = 0;
    //      }
    //      nextShootDirection =  ShootSequence[shootIndex];
    //      yield return new WaitForSeconds(0.8f);
    // }
}
