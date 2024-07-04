using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int CurrentScore = 0;
    private int MaxScore = 0;
    public int scoreMultiplier = 1;
    public TextMeshProUGUI ScoreTextField;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        ScoreTextField.gameObject.SetActive(true);
        UpdateScoreText(0);
    }

    public void AddScore(int score)
    {
        Debug.Log("Score Multiplier : " + scoreMultiplier);
        CurrentScore = CurrentScore + (score * scoreMultiplier);
        SetMaxScore(CurrentScore);
        UpdateScoreText(CurrentScore);
    }

    public void ReduceScore(int score)
    {
        if (CurrentScore > 0 && CurrentScore >= score)
            CurrentScore = CurrentScore - score;
        else
            CurrentScore = 0;
        SetMaxScore(CurrentScore);
        UpdateScoreText(CurrentScore);
    }

    public int GetCurrentScore()
    {
        return CurrentScore;
    }

    public void ResetLocalScore()
    {
        CurrentScore = 0;
        MaxScore = 0;
        UpdateScoreText(CurrentScore);
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
