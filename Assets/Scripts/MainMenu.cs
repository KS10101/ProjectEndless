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
    [SerializeField] private AudioClip ClickSFX;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private GameObject LeaderboardPanel;

    private void Start()
    {
        AudioManager.instance.PlayBGSound(bgmClip);
    }

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
        AudioManager.instance.PlaySFX(ClickSFX);
        SceneManager.LoadSceneAsync(1);
        this.gameObject.SetActive(false);
    }

    private void BuildLeaderboard()
    {
        AudioManager.instance.PlaySFX(ClickSFX);
        LeaderboardPanel.SetActive(true);
        //Leaderboard.instance.ClearScoresFromJson();
        Leaderboard.instance.CreateEntry();
    }

    private void ExitGame()
    {
        AudioManager.instance.PlaySFX(ClickSFX);
        Application.Quit();
    }
}
