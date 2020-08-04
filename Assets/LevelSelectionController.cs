using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelSelectionController : MonoBehaviour
{
    SceneNames sceneNames = new SceneNames();
    Dictionary<int, string> levelsScenesName =
    new Dictionary<int, string>();
    List<GameObject> menuOptions = new List<GameObject>();

     public  int OptionSelectedIndex = 0;

     float horizontalMove;

    public SaveData saveData;
     SelectMenuColors selectMenuColors = new SelectMenuColors();

     public AudioSource ChangeSelectedSound;
    // Start is called before the first frame update

    GameObject levelInfoLastTimeDisplay;
    GameObject levelInfoBestTimeDisplay;
    void Start()
    {
     
          saveData = SaveSystem.LoadSave();
           
           saveData.completedLevels.Sort();
       
       levelsScenesName = sceneNames.levelsScenesName;
       for(int i=0; i<levelsScenesName.Count;i++){           
           int optionNumber = (i+1);
            menuOptions.Add(GameObject.Find("Option"+optionNumber));
       }
       levelInfoLastTimeDisplay = GameObject.Find("LastTime");
       levelInfoBestTimeDisplay = GameObject.Find("BestTime");
        UpdateOptions();
        UpdateLevelInfo();
    }

    // Update is called once per frame
    void Update()
    {
       listenHorizontalMove();

       

         if(horizontalMove!=0){
            ChangeSelectedSound.Play();
            if(horizontalMove==-1 && OptionSelectedIndex!=0){
                OptionSelectedIndex = OptionSelectedIndex - 1;
                 UpdateOptions();
                 UpdateLevelInfo();
            }
            if(horizontalMove==1 && OptionSelectedIndex<menuOptions.Count - 1){
                OptionSelectedIndex = OptionSelectedIndex + 1;
                 UpdateOptions();
                 UpdateLevelInfo();
            }
        }
    }

    void listenHorizontalMove(){
        if(Input.GetButtonDown("Horizontal")){
            horizontalMove =Input.GetAxisRaw("Horizontal");
        }else{
           horizontalMove = 0;
        } 
    }

    void ChangeColorLevelUnlocked(GameObject optionObject){
        getChildBorda(optionObject).SetActive(true);
        getChildBorda(optionObject).GetComponent<Image>().color = selectMenuColors.borderUnlocked;
        getChildTexto(optionObject).GetComponent<TextMeshProUGUI>().color = selectMenuColors.textUnlocked;
         getChildSeletor(optionObject).SetActive(false);
         
    }

    void ChangeColorLevelLocked(GameObject optionObject){
        getChildBorda(optionObject).SetActive(true);
        getChildBorda(optionObject).GetComponent<Image>().color = selectMenuColors.borderLocked;
        getChildTexto(optionObject).GetComponent<TextMeshProUGUI>().color = selectMenuColors.textLocked;
        getChildSeletor(optionObject).SetActive(false);
         
    }
     void ChangeColorOptionSelected(GameObject optionObject){
       
        getChildSeletor(optionObject).SetActive(true);
        getChildBorda(optionObject).SetActive(false);
        getChildTexto(optionObject).GetComponent<TextMeshProUGUI>().color = selectMenuColors.active;       
    }

     void UpdateOptions(){
        //  Debug.Log(OptionSelectedIndex);
        ChangeColorOptionSelected(menuOptions[OptionSelectedIndex]);
        for(int i = 0; i<menuOptions.Count; i++){
        
         if(i!=OptionSelectedIndex){
             //if n esta no save e n eh igual ao ultimo do save mais 1 
             //entao pinte de locked
             //senao pinte de unlocked
          
             if(saveData.completedLevels.Find(x => x.levelIndex == i)  != null ){
                  ChangeColorLevelUnlocked( menuOptions[i]);
             }        else{
                    ChangeColorLevelLocked( menuOptions[i]);
             }
              int lastLevelSavedIndex = saveData.completedLevels[saveData.completedLevels.Count-1].levelIndex;  
             if(lastLevelSavedIndex +1 < menuOptions.Count){
                 if(i==lastLevelSavedIndex + 1){
                      ChangeColorLevelUnlocked( menuOptions[i]);
                 }
             }
             if(i==0){
                   ChangeColorLevelUnlocked( menuOptions[i]);
             }  
         }   
        }
    }

    void UpdateLevelInfo(){
            //Caso n esteja no save entao nao mostra info
            // caso esteja, atualiza info
            GameObject StarCountingGameObjectLast = levelInfoLastTimeDisplay.transform.GetChild(3).gameObject; //starscounting
            GameObject TimeGameObjectLast = levelInfoLastTimeDisplay.transform.GetChild(1).gameObject; //time

            GameObject StarCountingGameObjectBest  = levelInfoBestTimeDisplay.transform.GetChild(3).gameObject;
            GameObject TimeGameObjectBest = levelInfoBestTimeDisplay.transform.GetChild(1).gameObject;
            LevelData levelSelectedInfo = saveData.completedLevels.Find(x => x.levelIndex == OptionSelectedIndex);
            // Debug.Log(levelSelectedInfo);
            // Debug.Log(OptionSelectedIndex);
          if( levelSelectedInfo != null ){                
                StarCountingGameObjectLast.GetComponent<TextMeshProUGUI>().text = levelSelectedInfo.numberStarsColected.ToString();
                TimeGameObjectLast.GetComponent<TextMeshProUGUI>().text = FormatTimeToDisplay(levelSelectedInfo.timeLevel);     

                StarCountingGameObjectBest.GetComponent<TextMeshProUGUI>().text = levelSelectedInfo.numberStarsColectedBestTime.ToString();
                TimeGameObjectBest.GetComponent<TextMeshProUGUI>().text = FormatTimeToDisplay(levelSelectedInfo.BestTimeLevel);     

             }else{
                StarCountingGameObjectLast.GetComponent<TextMeshProUGUI>().text = "0";
                TimeGameObjectLast.GetComponent<TextMeshProUGUI>().text = "--:--:--";  
                StarCountingGameObjectBest.GetComponent<TextMeshProUGUI>().text = "0";
                TimeGameObjectBest.GetComponent<TextMeshProUGUI>().text = "--:--:--";  
             }
    }
    GameObject getChildBorda(GameObject optionObject){
        return optionObject.transform.GetChild(0).gameObject;
    }
    GameObject getChildSeletor(GameObject optionObject){
        return optionObject.transform.GetChild(1).gameObject;
    }

     GameObject getChildTexto(GameObject optionObject){
        return optionObject.transform.GetChild(2).gameObject;
    }

    string FormatTimeToDisplay(float timeLevel){
        int hours = (int)(timeLevel / 3600f);
        int minutes = (int)(timeLevel / 60f);
        int seconds = (int)(timeLevel % 60f);
        int milliseconds = (int)(((timeLevel % 60f) * 1000f) % 1000); //Time.timeSinceLevelLoad * 6f
        
        string time = "";
        if( !string.Equals(hours.ToString("00"), "00")){
            time += hours.ToString ("00") + ":";
        }
       
        time += minutes.ToString ("00") + ":" + seconds.ToString ("00");
        // textComponentTimer.text = time;
        // textComponentTimerMilliSeconds.text =  milliseconds.ToString("000");
        return time+" "+milliseconds.ToString("000");
    }

}
