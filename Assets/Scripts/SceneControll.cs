using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{
    Scene sceneActive;
    GameObject Player;
    GameObject GamePausedScreen;

    bool IsPaused = false;
     public GameObject CanvasTransition;
    Dictionary<int, string> levelsScenesName =
    new Dictionary<int, string>();
     void Start()
    {
      
        SceneNames sceneNames = new SceneNames();
        levelsScenesName = sceneNames.levelsScenesName;
        // levelsScenesName[0]="Tutorial_scene";
        // levelsScenesName[1]="SampleScene";

        Player = GameObject.Find("Player");
        GameObject canvas = GameObject.Find("Canvas");
        if(canvas!=null){
             GamePausedScreen = canvas.transform.GetChild(0).gameObject;
        }
       
        sceneActive = SceneManager.GetActiveScene();


        if(!sceneActive.name.Equals("titlescreen")){
            StartCoroutine(TransitionAnimationIn());
        }

    }

    void Update(){
        //Se player null então está em tela de menu (title screen ou phase_completed screen)
        if(Player==null){
            if(Input.anyKey){
               
                if(sceneActive.name.Equals("titlescreen")){
                    StartCoroutine(ChangeScene("MainMenuScreen"));
                }
                if(sceneActive.name.Equals("phase_complete")){
                    //mandar para proxima fase
                    nextLevel();
                }
                 if(sceneActive.name.Equals("MainMenuScreen")){
                     MainMenu();                  
                }
                 if(sceneActive.name.Equals("LevelSelectionScreen")){
                    LevelSelectionMenu();                  
                }
            }
        }

    //PAUSE SYSTEM    
        if(Player!=null){             
            if(Player.GetComponent<HealthController>().health<=0){              
                OnPlayerDead();
            }
            if(Input.GetButtonDown("Cancel")){
                //evento game paused
                TogglePause();
            }
            if(IsPaused){
                PauseMenu();
            }
        }

    }

     

    void nextLevel(){
         int lastPhase = PlayerPrefs.GetInt("lastPhase", 0);  
         Debug.Log("Last phase var "+lastPhase);
         Debug.Log(levelsScenesName[lastPhase+1]);
         try
        {
              StartCoroutine(ChangeScene(levelsScenesName[lastPhase+1]));
         }catch(KeyNotFoundException e){

         }
        
    }

    void PauseMenu(){
        if(Input.GetButtonDown("Submit")){
            GameObject MenuOptions = GameObject.Find("MenuOptions");
            int OptionSelectedIndex = MenuOptions.GetComponent<PauseMenuController>().OptionSelectedIndex;
            if(OptionSelectedIndex==0){
                TogglePause();
            }
            if(OptionSelectedIndex==1){
                StartCoroutine(ChangeScene(sceneActive.name));
                TogglePause();
            }
        }
    }

    //MAIN MENU ACTIONS
    void MainMenu(){
        GameObject MenuOptions = GameObject.Find("MenuOptions");
        if(MenuOptions!=null){
            //Pegando valor do index selecionado
            int OptionSelectedIndex = MenuOptions.GetComponent<MainMenuController>().OptionSelectedIndex;

            //Indo para outra tela pelo valor selecionado
            //a option 0 eh: create new game
            if(OptionSelectedIndex==0 && Input.GetButtonDown("Submit")){
                SaveSystem.CreateSave();
                StartCoroutine(ChangeScene(levelsScenesName[0]));
            }
        //op 1: load game    
            if(OptionSelectedIndex==1 && Input.GetButtonDown("Submit")){
                SaveSystem.LoadSave();
                StartCoroutine(ChangeScene("LevelSelectionScreen"));
            }

        }
    }
    //LEVEL SELECTION MENU
    void LevelSelectionMenu(){
        
        if(Input.GetButtonDown("Submit")){

            GameObject LevelSelection = GameObject.Find("LevelSelection");
            int OptionSelectedIndex = LevelSelection.GetComponent<LevelSelectionController>().OptionSelectedIndex;
            SaveData saveData = LevelSelection.GetComponent<LevelSelectionController>().saveData;
            foreach(LevelData levelData in saveData.completedLevels){
                Debug.Log(levelData.levelIndex);
            }
            int lastLevelSavedIndex = saveData.completedLevels[saveData.completedLevels.Count-1].levelIndex; 

                //SaveSystem.CreateSave();
                if(saveData.completedLevels.Find(x=> x.levelIndex == OptionSelectedIndex) != null 
                || OptionSelectedIndex == 0
                || OptionSelectedIndex == lastLevelSavedIndex + 1){
                    Debug.Log("Unlocked");                     
                   StartCoroutine(ChangeScene(levelsScenesName[OptionSelectedIndex]));
                }else{                     
                    Debug.Log("Locked");
                }
        }
    }
    public void OnLevelCompleted(){
        StartCoroutine(ChangeScene("phase_complete"));
    }

    void OnPlayerDead(){        
         StartCoroutine(ChangeScene(sceneActive.name,1f));
    }

    IEnumerator ChangeScene(string scene) 
{   
        //SceneManager.UnloadSceneAsync(sceneActive.name);
         StartCoroutine(TransitionAnimationOut());
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
       
        
}

    IEnumerator ChangeScene(string scene, float timetowait) 
{       TransitionAnimationOut();
        //SceneManager.UnloadSceneAsync(sceneActive.name);
       
        yield return new WaitForSeconds(timetowait);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
      
        
}

 IEnumerator TransitionAnimationOut(){
        CanvasTransition.SetActive(true);
        CanvasTransition.GetComponent<ScreenTransitionController>().AnimationOut();
        yield return null;
 }

  IEnumerator TransitionAnimationIn(){
        CanvasTransition.SetActive(true);
        CanvasTransition.GetComponent<ScreenTransitionController>().AnimationIn();
        yield return null;
 }

void TogglePause(){
    if(IsPaused){
        ResumeGame();
        IsPaused=false;
    }else{
        PauseGame();
        IsPaused=true;
    }
}
void PauseGame()
    {
        if(GamePausedScreen!=null){
            GamePausedScreen.SetActive(true);
        }
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        if(GamePausedScreen!=null){
            GamePausedScreen.SetActive(false);
        }
       
        Time.timeScale = 1;
    }


}