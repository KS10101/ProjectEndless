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
    [SerializeField] private Button submitButton;

    [SerializeField] private TMP_InputField nameInput;

    [SerializeField] private GameObject inGameUICanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject highscoreCanvas;
    [SerializeField] private GameObject leaderboardCanvas;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;

    private void Start()
    {
        OnGameOver();
    }

    private string SetName()
    {
        string inputText = nameInput.text;
        return inputText;
    }

    private void OnSubmitButtonClick()
    {
        if (SetName() != null)
        {
            string nameText = SetName();
            int score = ScoreManager.instance.GetCurrentScore();

            Debug.Log($"Name - {SetName()} Score - {score} Current Score - {ScoreManager.instance.GetCurrentScore()}");
            if (Leaderboard.instance.GetEntryCount() < 8)
                Leaderboard.instance.AddScoreCard(nameText, score);
            else if (Leaderboard.instance.GetEntryCount() >= 8)
                Leaderboard.instance.AddScoreAtEnd(nameText, score);
            highscoreCanvas.SetActive(false);
            currentScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            gameOverCanvas.SetActive(true);
        }

    }

    public void OnGameOver()
    {
        inGameUICanvas.SetActive(false);
        if ((Leaderboard.instance.GetEntryCount() < 8 && ScoreManager.instance.GetCurrentScore() != 0) ||
            (Leaderboard.instance.GetEntryCount() == 8 && ScoreManager.instance.GetCurrentScore() > Leaderboard.instance.GetLowestScore()))
        {
            highScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            highscoreCanvas.SetActive(true);

        }
        else
        {
            currentScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            highscoreCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }

    }

    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(GoToMenu);
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        leadboardButton.onClick.AddListener(BuildLeaderboard);
    }

    private void OnDisable()
    {
        playAgainButton.onClick.RemoveListener(Restart);
        mainMenuButton.onClick.RemoveListener(GoToMenu);
        submitButton.onClick.RemoveListener(OnSubmitButtonClick);
        leadboardButton.onClick.RemoveListener(BuildLeaderboard);
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

    private void BuildLeaderboard()
    {
        gameOverCanvas.SetActive(false);
        highscoreCanvas.SetActive(false);
        leaderboardCanvas.SetActive(true);
        //Leaderboard.instance.ClearScoresFromJson();
        //Leaderboard.instance.SaveScoresToJSON();
        Leaderboard.instance.CreateEntry();
    }
}