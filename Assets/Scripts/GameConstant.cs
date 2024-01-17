using UnityEngine;

public static class GameConstant
{
    public static int TotalGems
    {
        set=>PlayerPrefs.SetInt("GemsCollect",value);
        get => (PlayerPrefs.GetInt("GemsCollect",1));
    }

    public static int WaveNo=1;
    public static int noEnemiesKilled=0;
}
