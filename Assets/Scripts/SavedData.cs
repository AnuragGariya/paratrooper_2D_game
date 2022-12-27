using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavedData
{
    public static int HighScore
    {
        get => PlayerPrefs.GetInt("highScore", 0);
        set => PlayerPrefs.SetInt("highScore", value);
    }
}
