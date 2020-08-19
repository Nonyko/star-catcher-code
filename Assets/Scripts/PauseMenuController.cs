using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{   
   
    List<GameObject> menuOptions = new List<GameObject>();
    public  int OptionSelectedIndex = 0;

    public AudioSource ChangeSelectedSound;

    float verticalMove;
    // Start is called before the first frame update
    void Start()
    {
        
        menuOptions.Add(GameObject.Find("Option1"));
        menuOptions.Add(GameObject.Find("Option2"));
        menuOptions.Add(GameObject.Find("Option3"));
        menuOptions.Add(GameObject.Find("Option4"));
    }

float OldVerticalMove = 0;
bool ChangedToOne = false;
 bool VerticalMovementChangedToOne(float verticalMove){
        float NewVerticalMove = verticalMove;
        bool changedToOne = false;
        if(OldVerticalMove!=1 && OldVerticalMove!=-1){
            if(NewVerticalMove==1 || NewVerticalMove==-1){
               //mudou para 0
            //    Debug.Log("MUDOU PRA 1 OU -1");
                OldVerticalMove = NewVerticalMove;
                changedToOne =  true;
                // StartCoroutine(ChangedToOneCoroutine());
                ChangedToOne = true;
                //  Debug.Log(changedToOne);
            }
        }
        OldVerticalMove = NewVerticalMove;
        return changedToOne;
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovementChangedToOne(Input.GetAxisRaw("Vertical"));
        if(Input.GetButtonDown("Vertical") || ChangedToOne){
             verticalMove =Input.GetAxisRaw("Vertical");
             ChangedToOne = false;
        }else{
            verticalMove = 0;
        }       
        //Debug.Log(verticalMove);//1 up, -1 down
        if(verticalMove!=0){
            ChangeSelectedSound.Play();
            if(verticalMove==1 && OptionSelectedIndex!=0){
                OptionSelectedIndex = OptionSelectedIndex - 1;
                 ShowOptionSelected();
            }
            if(verticalMove==-1 && OptionSelectedIndex<menuOptions.Count - 1){
                OptionSelectedIndex = OptionSelectedIndex + 1;
                 ShowOptionSelected();
            }
        }
    }

    void ShowOptionSelected(){
        //Debug.Log(OptionSelectedIndex);
        menuOptions[OptionSelectedIndex].transform.GetChild(0).gameObject.SetActive(true);
        for(int i = 0; i<menuOptions.Count; i++){
         if(i!=OptionSelectedIndex){
             menuOptions[i].transform.GetChild(0).gameObject.SetActive(false);
         }   
        }
    }
}
