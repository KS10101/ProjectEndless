using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelGameManager : MonoBehaviour
{
    public static LevelGameManager instance;

    [SerializeField] private Button playAgain;
    [SerializeField] private Button mainMenu;
    [SerializeField] private Button leadboard;

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject highscoreCanvas;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private void OnGameOver()
    {
        


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
        playAgain.onClick.AddListener(Restart);
        mainMenu.onClick.AddListener(GoToMenu);
        //leaderboard
    }

    private void OnDisable()
    {
        playAgain.onClick.RemoveListener(Restart);
        mainMenu.onClick.RemoveListener(GoToMenu);
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