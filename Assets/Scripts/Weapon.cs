using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
   public Transform FirePoint;
   public GameObject bulletPrefab;

   public AudioSource ShootSound;

   public UnityEvent OnShootEvent;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){            
            Shoot();
        }
    }
    void Shoot(){
        OnShootEvent.Invoke();
        Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        ShootSound.Play();
    }
}
