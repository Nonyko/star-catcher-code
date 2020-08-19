using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public AudioSource ExplosionSound;
    // Start is called before the first frame update
    void Start()
    {
        ExplosionSound.Play();
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator  Destroy(){
        yield return new WaitForSeconds(0.5f);
         Destroy(gameObject);
    }
}
