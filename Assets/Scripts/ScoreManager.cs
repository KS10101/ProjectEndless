using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int CurrentScore;
    private int MaxScore;
    public TextMeshProUGUI ScoreTextField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void AddScore(int score)
    {
        CurrentScore = CurrentScore + score;
        UpdateScoreText(CurrentScore);
    }

    public void ReduceScore(int score)
    {
        if (CurrentScore > 0 && CurrentScore >= score)
            CurrentScore = CurrentScore - score;
        else
            CurrentScore = 0;

        UpdateScoreText(CurrentScore);
    }

    public int GetCurrentScore()
    {
        return CurrentScore;
    }

    public int GetMaxScore()
    {
        return MaxScore;
    }

    public void SetMaxScore(int score)
    {
        if(score > MaxScore)
            MaxScore = score;
    }

    public void UpdateScoreText(int score)
    {
        ScoreTextField.text = score.ToString();
    }
}
