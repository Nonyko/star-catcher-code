using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData{
    public List<LevelData> completedLevels = new List<LevelData>();

    public void AddLevel(LevelData levelData){
      // LevelData level = completedLevels.Find();
      int index = completedLevels.FindIndex(x => x.levelIndex == levelData.levelIndex);

        if(index == -1){
            completedLevels.Add(levelData);
        }else{
           
            float bestTime = 0;
            int bestTimeStarsCouting = 0;
            LevelData OldLevelInfo = completedLevels[index];
           
            
            if(OldLevelInfo.BestTimeLevel==0 || OldLevelInfo.BestTimeLevel>levelData.timeLevel){
            levelData.BestTimeLevel = levelData.timeLevel;
            levelData.numberStarsColectedBestTime = levelData.numberStarsColected;           
            }else{
                levelData.BestTimeLevel = OldLevelInfo.BestTimeLevel;
                levelData.numberStarsColectedBestTime = OldLevelInfo.numberStarsColected;
            }
            completedLevels[index] = levelData;
        }
    }
    
}