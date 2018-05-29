using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public Text scoreText;

    public static int totalScore;
    private void Start()
    {
        scoreText = GetComponent<Text>();
        Reset();
    }
    public void AddScore(int score)
    {
        totalScore = totalScore + score;
        scoreText.text = totalScore.ToString();
    }
    public static void Reset()
    {
        totalScore = 0;
    }
}
