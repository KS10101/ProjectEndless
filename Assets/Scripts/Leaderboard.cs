using Dreamteck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] private Transform ScoreCardsContainer;
    [SerializeField] private GameObject EntryPrefab;
    private List<ScoreCard> _ScoreCardEntries = new List<ScoreCard>(8);
    ScoreCard[] ScoreEntriesArray;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private AudioClip clickSFX;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        UpdateList();
        SaveScoresToJSON();
    }

    private void OnEnable()
    {
        backButton.onClick.AddListener(OnClickBackBut);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveListener(OnClickBackBut);
    }

    private void OnClickBackBut()
    {
        AudioManager.instance.PlaySFX(clickSFX);
        leaderboardPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void DeleteGeneratedEntries()
    {
        int entriesCount = ScoreCardsContainer.childCount;

        if (entriesCount == 0) return;

        for (int i = 0; i < entriesCount; i++)
        {
            Destroy(ScoreCardsContainer.GetChild(i).gameObject);
        }
    }

    public void UpdateList()
    {
        if (LoadScoresFromJSON() == null)
        {
            _ScoreCardEntries = new List<ScoreCard>();
        }
        else
            _ScoreCardEntries = LoadScoresFromJSON().ScoreCardEntries;
        SaveScoresToJSON();
    }

    public void CreateEntry()
    {
        DeleteGeneratedEntries();
        UpdateList();
        ScoreEntriesArray = SortList(_ScoreCardEntries);

        if (ScoreEntriesArray == null) return;
        // if score card entries is not null 
        Debug.Log($"Array Size: {ScoreEntriesArray.Count()}");
        for (int i = 0; i < ScoreEntriesArray.Count(); i++)
        {
            GameObject entry = Instantiate(EntryPrefab, ScoreCardsContainer);
            entry.SetActive(true);
            entry.GetComponent<ScoreEntriesSetter>().SetDataEntries(i+1, ScoreEntriesArray[i].Name, ScoreEntriesArray[i].Score);
            
        }

        SaveScoresToJSON();
    }

    public void AddScoreAtEnd(string name, int Score)
    {
        UpdateList();
        ScoreCard[] temp = SortList(_ScoreCardEntries);
        temp[temp.Count() - 1] = new ScoreCard{Name = name, Score = Score};

        _ScoreCardEntries = temp.ToList();
        SaveScoresToJSON();

    }

    public int GetEntryCount()
    {
        UpdateList();

        return _ScoreCardEntries.Count;
    }

    public int GetHighestScore()
    {
        UpdateList();
        ScoreCard[] entries = SortList(_ScoreCardEntries);

        return entries[0].Score;
    }

    public int GetLowestScore()
    {
        UpdateList();
        ScoreCard[] entries = SortList(_ScoreCardEntries);

        return entries[entries.Count() - 1].Score;
    }

    public ScoreCard[] SortList(List<ScoreCard> Scorelist)
    {
        if (Scorelist == null || Scorelist.Count == 0) return null;

        var entries = Scorelist.OrderByDescending(s => s.Score);

        return entries.ToArray();
    }

    public void AddScoreCard(ScoreCard _scoreCard)
    {
        _ScoreCardEntries.Add(_scoreCard);
        UpdateList();
        SaveScoresToJSON();
    }

    public void AddScoreCard(string _name, int _score)
    {
        ScoreCard _scorecard = new ScoreCard { Name = _name, Score = _score };
        UpdateList();
        _ScoreCardEntries.Add(_scorecard);
        
        SaveScoresToJSON();
        Debug.Log("Score Saved");
    }

    public ScoreCardEntry LoadScoresFromJSON()
    {
        if (!PlayerPrefs.HasKey("ScoreCardEntries") || PlayerPrefs.GetString("ScoreCardEntries") == null) return null;
        
            
        string json = PlayerPrefs.GetString("ScoreCardEntries");
        ScoreCardEntry Entries = JsonUtility.FromJson<ScoreCardEntry>(json);

        return Entries;
    }

    public void SaveScoresToJSON()
    {
        ScoreCardEntry Entries = new ScoreCardEntry {ScoreCardEntries = _ScoreCardEntries };
        string json = JsonUtility.ToJson(Entries);
        PlayerPrefs.SetString("ScoreCardEntries", json);
        PlayerPrefs.Save();
        Debug.Log($"Saved to JSon" +
            $"{PlayerPrefs.GetString("ScoreCardEntries")}");
    }

    public void ClearScoresFromJson()
    {
        PlayerPrefs.SetString("ScoreCardEntries", "");
    }
}

public class ScoreCardEntry
{
    public List<ScoreCard> ScoreCardEntries;
}

[System.Serializable]
public class ScoreCard
{
    public int Rank = 0;
    public string Name;
    public int Score;
}
