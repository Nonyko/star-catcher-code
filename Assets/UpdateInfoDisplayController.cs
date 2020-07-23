using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpdateInfoDisplayController : MonoBehaviour
{   
     public TextMeshProUGUI textComponentNumberStars;
     public TextMeshProUGUI textComponentTimer;
     public TextMeshProUGUI textComponentTimerMilliSeconds;
    GameObject gameStatus;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus = GameObject.Find("GameStatus");
        if(gameStatus==null){
            Debug.LogError("Nao encontro objeto chamado GameStatus");
            this.enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus!= null){
            LevelInformation info = gameStatus.GetComponent<LevelInformation>();            
            textComponentNumberStars.text = info.numberStarsColected.ToString();
           FormatTimeAndDisplay(info);
        }
    }

    void FormatTimeAndDisplay(LevelInformation info){
        int hours = (int)(info.timeLevel / 3600f);
        int minutes = (int)(info.timeLevel / 60f);
        int seconds = (int)(info.timeLevel % 60f);
        int milliseconds = (int)(((info.timeLevel % 60f) * 1000f) % 1000); //Time.timeSinceLevelLoad * 6f
        
        string time = "";
        if( !string.Equals(hours.ToString("00"), "00")){
            time += hours.ToString ("00") + ":";
        }
       
        time += minutes.ToString ("00") + ":" + seconds.ToString ("00");
        textComponentTimer.text = time;
        textComponentTimerMilliSeconds.text =  milliseconds.ToString("000");
    }
}
