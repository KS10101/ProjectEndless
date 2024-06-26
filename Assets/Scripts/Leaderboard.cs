using Dreamteck;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] private Transform ScoreCardsContainer;
    [SerializeField] private GameObject EntryPrefab;
    private List<ScoreCard> _ScoreCardEntries = new List<ScoreCard>(8);



    private void Awake()
    {
        if (instance == null)
            instance = this;
        //LoadScoresFromJSON();
        _ScoreCardEntries = LoadScoresFromJSON().ScoreCardEntries;
        AddScoreCard("al-chan", 15);
        AddScoreCard("Gamora-Kun", 12);
        AddScoreCard("Divyanshu-Sama", 1);
        SaveScoresToJSON();
        //AddScoreCard("Kartik cha", 5);
        //SaveScoresToJSON();
        //CreateEntry();
    }

    private void Start()
    {
        CreateEntry();
        ScoreCard e1 = new ScoreCard { Name = "ABC", Score = 999 };
        AddScoreCard(e1);

        AddScoreCard("BAD", Random.Range(0, 999));
        Debug.Log("Score ADDED");
    }

    private void CreateEntry()
    {
        _ScoreCardEntries = LoadScoresFromJSON().ScoreCardEntries;
        // if score card entries is not null 
        for (int i = 0; i < _ScoreCardEntries.Count; i++)
        {
            GameObject entry = Instantiate(EntryPrefab, ScoreCardsContainer);
            entry.SetActive(true);
            entry.GetComponent<ScoreEntriesSetter>().SetDataEntries(_ScoreCardEntries[i].Rank, _ScoreCardEntries[i].Name, _ScoreCardEntries[i].Score);

        }
    }



    public void AddScoreCard(ScoreCard _scoreCard)
    {
        _ScoreCardEntries.Add(_scoreCard);

    }

    public void AddScoreCard(string _name, int _score)
    {
        ScoreCard _scorecard = new ScoreCard { Name = _name, Score = _score };
        _ScoreCardEntries.Add(_scorecard);
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
        Debug.Log(PlayerPrefs.GetString("ScoreCardEntries"));
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
