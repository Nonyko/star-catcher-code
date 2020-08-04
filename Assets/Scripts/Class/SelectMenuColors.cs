using UnityEngine;
public class SelectMenuColors{
    public Color borderUnlocked;
    public Color textUnlocked;

    public Color borderLocked;
    public Color textLocked;

    public Color active;
    public SelectMenuColors(){
        borderUnlocked = new Color(1,1,1,1);
        textUnlocked = new Color(0.8396226f,0.3524831f,0.796501f,1);
        borderLocked = new Color(0.4523251f,0.3122997f,0.5471698f,1);
        textLocked = new Color(0.4729094f,0.4033019f,0.5f,1);
        active = new Color(0.8584906f,0.2227216f,0.5002145f,1);
    }
}