using Dreamteck;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


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
        AddScoreCard("al-chan", 19);
        AddScoreCard("Gamora-Kun", 2);
        AddScoreCard("Divyanshu-Sama", 100);
        AddScoreCard("Vidhayak ji", 500);
        AddScoreCard("Sachiv ji", 200);
        AddScoreCard("Kartik cha", 5);
        AddScoreCard("Mishra ji", 1000);
    }

    public void UpdateList()
    {
        SaveScoresToJSON();
        if(LoadScoresFromJSON().ScoreCardEntries == null)
        {
            _ScoreCardEntries = new List<ScoreCard>();
        }
        else
            _ScoreCardEntries = LoadScoresFromJSON().ScoreCardEntries;
    }

    private void Start()
    {

    }

    public void CreateEntry()
    {
        UpdateList();
        var entries = _ScoreCardEntries.OrderByDescending(s => s.Score);
        ScoreEntriesArray = entries.ToArray();
        
        // if score card entries is not null 
        for (int i = 0; i < ScoreEntriesArray.Count(); i++)
        {
            GameObject entry = Instantiate(EntryPrefab, ScoreCardsContainer);
            entry.SetActive(true);
            entry.GetComponent<ScoreEntriesSetter>().SetDataEntries(i+1, ScoreEntriesArray[i].Name, ScoreEntriesArray[i].Score);

        }
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
        _ScoreCardEntries.Add(_scorecard);
        UpdateList();
        SaveScoresToJSON();
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
