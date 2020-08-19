using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
   public Transform FirePoint;
   public GameObject bulletPrefab;

   public AudioSource ShootSound;
   public AudioSource EmptyEnergySound;

   public UnityEvent OnShootEvent;

    GameObject Player;

    GameObject EnergyBar;
   void Start(){
       Player = GameObject.Find("Player");
       EnergyBar = GameObject.Find("Energy Bar");
   
   }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") 
        && !Player.GetComponent<PlayerMovement>().isDead 
           && !Player.GetComponent<PlayerMovement>().IsEntering){            
            Shoot();
        }
    }
    void Shoot(){
        //Verifica se a energia atual é maior ou igual q o custo para atirar
        if(EnergyBar.GetComponent<EnergyBarController>().EnergyNow>=EnergyBar.GetComponent<EnergyBarController>().ShootCost){
            OnShootEvent.Invoke();
            Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
            ShootSound.Play();
        }else{
            EmptyEnergySound.Play();
        }
        
    }
}
