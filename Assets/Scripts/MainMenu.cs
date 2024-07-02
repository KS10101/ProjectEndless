using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button LeaderboardButton;
    [SerializeField] private Button ExitButton;

    [SerializeField] private GameObject LeaderboardPanel;

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(PlayGame);
        LeaderboardButton.onClick.AddListener(BuildLeaderboard);
        ExitButton.onClick.AddListener(ExitGame);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(PlayGame);
        LeaderboardButton.onClick.RemoveListener(BuildLeaderboard);
        ExitButton.onClick.RemoveListener(ExitGame);
    }

    private void PlayGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        this.gameObject.SetActive(false);
    }

    private void BuildLeaderboard()
    {
        LeaderboardPanel.SetActive(true);
        //Leaderboard.instance.ClearScoresFromJson();
        Leaderboard.instance.CreateEntry();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
