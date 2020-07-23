using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCountDisplayController : MonoBehaviour
{
    public int starsCollected = 0;
    public TextMeshProUGUI textComponent;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       textComponent.text = starsCollected.ToString();
    }

    public void OnStarCollected(){
        
         animator.SetBool("PlayAnimation", true);
         starsCollected += 1;
         StartCoroutine(StopTextAnimation());
    }

    // Update is called once per frame
    void Update()
    {
      
           textComponent.text = starsCollected.ToString();
          
    }

    IEnumerator StopTextAnimation() 
    {
        yield return new WaitForSeconds(.4f);
        animator.SetBool("PlayAnimation", false);
    }

}
