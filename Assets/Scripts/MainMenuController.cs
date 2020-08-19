using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Vertical")){
             verticalMove =Input.GetAxisRaw("Vertical");
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
        Debug.Log(OptionSelectedIndex);
        menuOptions[OptionSelectedIndex].transform.GetChild(0).gameObject.SetActive(true);
        for(int i = 0; i<menuOptions.Count; i++){
         if(i!=OptionSelectedIndex){
             menuOptions[i].transform.GetChild(0).gameObject.SetActive(false);
         }   
        }
    }
}
