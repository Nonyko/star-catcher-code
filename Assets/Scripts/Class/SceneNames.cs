using System.Collections;
using System.Collections.Generic;

public class SceneNames{
 public Dictionary<int, string> levelsScenesName =
    new Dictionary<int, string>();

    public SceneNames(){
        levelsScenesName[0]="Tutorial_scene";
        levelsScenesName[1]="Level_2";
        levelsScenesName[2]="Level_3";
        levelsScenesName[3]="Level_4";
        levelsScenesName[4]="Level_5";
        //levelsScenesName[4]="SampleScene";
    }

}