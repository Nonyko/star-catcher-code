using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class timeDisplayController : MonoBehaviour
{
     public TextMeshProUGUI textComponentTimer;
     public TextMeshProUGUI textComponentTimerMilliSeconds;
     
     public float timeLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate(){
        // float seconds = Mathf.RoundToInt(Time.time%60);
        // float miliseconds = (int)(Time.time * 1000f) % 1000;
        int hours = (int)(Time.timeSinceLevelLoad / 3600f);
        int minutes = (int)(Time.timeSinceLevelLoad / 60f);
        int seconds = (int)(Time.timeSinceLevelLoad % 60f);
        int milliseconds = (int)(((Time.timeSinceLevelLoad % 60f) * 1000f) % 1000); //Time.timeSinceLevelLoad * 6f
        
        string time = "";
        if( !string.Equals(hours.ToString("00"), "00")){
            time += hours.ToString ("00") + ":";
        }
       
        time += minutes.ToString ("00") + ":" + seconds.ToString ("00");
        textComponentTimer.text = time;
        textComponentTimerMilliSeconds.text =  milliseconds.ToString("000");
        timeLevel = Time.timeSinceLevelLoad;
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
