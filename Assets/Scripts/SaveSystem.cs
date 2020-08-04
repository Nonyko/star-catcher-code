using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
 public static SaveData saveData  = new SaveData();

 public static string SaveName = "save"; 
 public static void CreateSave(){
     BinaryFormatter formatter = new BinaryFormatter();
     
     string path = Application.persistentDataPath + "/"+SaveName+".bin";

     FileStream stream = new FileStream(path, FileMode.Create);
     formatter.Serialize(stream, saveData);
     stream.Close();
 }

 public static void SaveLevel(LevelInformation info){
     BinaryFormatter formatter = new BinaryFormatter();
     
     string path = Application.persistentDataPath + "/"+SaveName+".bin";
     FileStream stream = new FileStream(path, FileMode.Create);

     LevelData levelData = new LevelData(info);
  

        saveData.AddLevel(levelData);

        formatter.Serialize(stream, saveData);
        stream.Close();
 }

 public static SaveData LoadSave(){
     string path = Application.persistentDataPath + "/"+SaveName+".bin";
     if(File.Exists(path)){
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);
           saveData = formatter.Deserialize(stream) as SaveData;
           stream.Close();
            Debug.Log("Game Loaded!");
        //    foreach(LevelData level in saveData.completedLevels){
        //        Debug.Log(level.levelIndex);
        //    }
           return saveData;
     }else{
         Debug.LogError("Theres no file in the path "+path);
         return null;
     }
 }

}