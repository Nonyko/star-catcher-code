using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInformation : MonoBehaviour
{
    
    public int numberStarsColected = 0;
    public float timeLevel = 0f;

    public int lastPhase = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        numberStarsColected = PlayerPrefs.GetInt("numberStarsColected", 0);
        timeLevel = PlayerPrefs.GetFloat("timeLevel", 0f);      
        lastPhase = PlayerPrefs.GetInt("lastPhase", 0);      
    }

    void OnDestroy(){
      //  Debug.Log("Saving the stuff");
        PlayerPrefs.SetInt("numberStarsColected", numberStarsColected);
        PlayerPrefs.SetFloat("timeLevel", timeLevel);
        PlayerPrefs.SetInt("lastPhase", lastPhase);
        SaveSystem.SaveLevel(this);
    }
    // Update is called once per frame
    void Update()
    {
     
    }

    public  void UpdateData(int phaseIndex){
      numberStarsColected =  GameObject.Find("StarCounterDisplay").GetComponent<StarCountDisplayController>().starsCollected;
      timeLevel = GameObject.Find("TimeDisplay").GetComponent<timeDisplayController>().timeLevel;
      lastPhase =  phaseIndex;
    }
}
