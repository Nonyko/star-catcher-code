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
    Dictionary<int, string> levelsScenesName =
    new Dictionary<int, string>();
     void Start()
    {
        levelsScenesName[0]="Tutorial_scene";
        levelsScenesName[1]="SampleScene";

        Player = GameObject.Find("Player");
        GameObject canvas = GameObject.Find("Canvas");
        if(canvas!=null){
             GamePausedScreen = canvas.transform.GetChild(0).gameObject;
        }
       
        sceneActive = SceneManager.GetActiveScene();
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
                      GameObject MenuOptions = GameObject.Find("MenuOptions");
                      if(MenuOptions!=null){
                          //Pegando valor do index selecionado
                         int OptionSelectedIndex = MenuOptions.GetComponent<MainMenuController>().OptionSelectedIndex;

                            //Indo para outra tela pelo valor selecionado
                         if(OptionSelectedIndex==0 && Input.GetButtonDown("Submit")){
                              StartCoroutine(ChangeScene(levelsScenesName[0]));
                         }

                      }
                    //mandar para proxima fase
                    //nextLevel();
                }
            }
        }

        if(Player!=null){             
            if(Player.GetComponent<HealthController>().health<=0){              
                OnPlayerDead();
            }
            if(Input.GetButtonDown("Cancel")){
                //evento game paused
                TogglePause();
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
    public void OnLevelCompleted(){
        StartCoroutine(ChangeScene("phase_complete"));
    }

    void OnPlayerDead(){        
         StartCoroutine(ChangeScene(sceneActive.name));
    }

    IEnumerator ChangeScene(string scene) 
{   
        //SceneManager.UnloadSceneAsync(sceneActive.name);
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
        
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