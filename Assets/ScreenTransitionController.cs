using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransitionController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        GameObject children = this.gameObject.transform.GetChild(0).gameObject;
        animator = children.GetComponent<Animator>();
        if(animator==null){
            Debug.LogError("ANIMATOR NOT FOUND");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationIn(){
        GameObject children = this.gameObject.transform.GetChild(0).gameObject;
        animator = children.GetComponent<Animator>();
        animator.SetTrigger("in");

    }

    public void AnimationOut(){
        GameObject children = this.gameObject.transform.GetChild(0).gameObject;
        animator = children.GetComponent<Animator>();
        animator.SetTrigger("out");

    }
}
