using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager instance;

    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button leadboardButton;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject highscoreCanvas;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private void OnGameOver()
    {
        if((Leaderboard.instance.GetEntryCount() < 8) || 
            (Leaderboard.instance.GetEntryCount() == 8 && ScoreManager.instance.GetCurrentScore() > Leaderboard.instance.GetLowestScore()))
        {
            highscoreCanvas.SetActive(true);

        }
        else
        {
            highscoreCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }


        currentScore.text = "Score: "+ScoreManager.instance.GetCurrentScore().ToString();
        //Trigger Game Over Screen 
        //if leaderBoard list count is < 8 then insert the current score at leaderboard.count index.
        //if leaderBoard list count is > 8
        //get current score and compare it with the last index value in the Sorted Leaderboard list.
        //if current score is less than last index value then dont insert value in leader board
        //else insert current score in leader board list.

    }
    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(GoToMenu);
        //leaderboard
    }

    private void OnDisable()
    {
        playAgainButton.onClick.RemoveListener(Restart);
        mainMenuButton.onClick.RemoveListener(GoToMenu);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene((int)SceneHandler.instance.sceneSelect);
    }


}