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

    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject highscoreCanvas;

    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI highScore;


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
            int score = 123;//ScoreManager.instance.GetCurrentScore();

            Debug.Log($"Name - {SetName()} Score - {score}");
            Leaderboard.instance.AddScoreCard(nameText, score);
            //highscoreCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }

    }
    public void OnGameOver()
    {

     

        if ((Leaderboard.instance.GetEntryCount() < 8) || 
            (Leaderboard.instance.GetEntryCount() == 8 && ScoreManager.instance.GetCurrentScore() > Leaderboard.instance.GetLowestScore()))
        {
            highScore.text = Leaderboard.instance.GetHighestScore().ToString();
            highscoreCanvas.SetActive(true);

        }
        else
        {
            currentScore.text = ScoreManager.instance.GetCurrentScore().ToString();
            highscoreCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
        }


        currentScore.text = "Score: "+ScoreManager.instance.GetCurrentScore().ToString();

    }
    private void OnEnable()
    {
        playAgainButton.onClick.AddListener(Restart);
        mainMenuButton.onClick.AddListener(GoToMenu);
        submitButton.onClick.AddListener(OnSubmitButtonClick);
        //leaderboard
    }

    private void OnDisable()
    {
        playAgainButton.onClick.RemoveListener(Restart);
        mainMenuButton.onClick.RemoveListener(GoToMenu);
        submitButton.onClick.RemoveListener(OnSubmitButtonClick);
        //leaderboard
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