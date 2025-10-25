using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;

    public static void AddScore(int points)
    {
        score += points;
        SaveScore();
        Debug.Log("Puntaje actual: " + score);
    }

    public static void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }

    public static void LoadScore()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }

  
    public static void ResetScore()
    {
        score = 0;
        PlayerPrefs.DeleteKey("Score");
    }
}
