using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class LevelData : IComparable<LevelData>{
    public int levelIndex;
    public int numberStarsColected = 0;
    public float timeLevel = 0f;

     public int numberStarsColectedBestTime = 0;
    public float BestTimeLevel = 0f;
    public LevelData(LevelInformation info){
        levelIndex = info.lastPhase;
        numberStarsColected = info.numberStarsColected;
        timeLevel = info.timeLevel;
        
    }

     public int CompareTo(LevelData levelData)
    {
          // A null value means that this object is greater.
        if (levelData == null)
            return 1;

        else
            return this.levelIndex.CompareTo(levelData.levelIndex);
    }
}