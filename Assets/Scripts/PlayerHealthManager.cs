using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;
 
    private int streak = 0;
    [SerializeField] private int desiredStreak = 3;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void MakeStreak()
    {
        streak++;
        if (streak >= desiredStreak)
        {
            streak = 0;
            PlayerController.instance.StopPlayer();
            LevelGameManager.instance.OnGameOver();
        }
    }

    public void CancelStreak()
    {
        if(streak >= 0)
        {
            streak = 0;
        }
    }

}
