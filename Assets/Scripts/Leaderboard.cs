using Dreamteck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] private Transform ScoreCardsContainer;
    [SerializeField] private GameObject EntryPrefab;
    private List<ScoreCard> _ScoreCardEntries = new List<ScoreCard>(8);
    ScoreCard[] ScoreEntriesArray;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        UpdateList();
        SaveScoresToJSON();
        //AddScoreCard("al-chan", 19);
        //AddScoreCard("Gamora-Kun", 200);
        //AddScoreCard("Divyanshu-Sama", 200);
        //AddScoreCard("Vidhayak ji", 200);
        //AddScoreCard("Dhruv-San", 200);
        //AddScoreCard("Kartik cha", 200);
        //AddScoreCard("Mishra ji", 1000);
        //AddScoreCard("Cat", 200);
    }

    public void UpdateList()
    {
        if (LoadScoresFromJSON().ScoreCardEntries == null)
        {
            _ScoreCardEntries = new List<ScoreCard>();
        }
        else
            _ScoreCardEntries = LoadScoresFromJSON().ScoreCardEntries;
        SaveScoresToJSON();
    }

    public void CreateEntry()
    {
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
