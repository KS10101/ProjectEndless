using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreakManager : MonoBehaviour
{
    public static StreakManager instance;
 
    private int streak = 0;
    [SerializeField] private int scoreStreak = 0;
    [SerializeField] private int desiredStreak = 3;
    [SerializeField] private int multiplier = 1; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void MakeHealthStreak()
    {
        streak++;
        if (streak >= desiredStreak)
        {
            streak = 0;
            PlayerController.instance.StopPlayer();
            LevelGameManager.instance.OnGameOver();
        }
    }

    public void MakeScoreStreak()
    {
        scoreStreak++;
        if (scoreStreak >= desiredStreak)
        {
            multiplier ++;
            ScoreManager.instance.scoreMultiplier = multiplier;

        }
    }

    public void CancelStreak()
    {
        if(streak >= 0)
        {
            streak = 0;
        }
    }

    public void CancelScoreStreak()
    {
        if (scoreStreak >= 0)
        {
            scoreStreak = 0;
            multiplier = 1;
        }
    }

}
