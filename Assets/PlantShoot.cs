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

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
     
       
    }
    void FixedUpdate(){
      
    }
    void Shoot(){
        OnPlantShootEvent.Invoke();  
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        //ShootSound.Play();
    }
}
