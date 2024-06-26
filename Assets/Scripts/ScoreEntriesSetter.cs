using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEntriesSetter : MonoBehaviour
{
    private int rank;
    private string _name;
    private int score;

    [SerializeField] private TextMeshProUGUI rankField;
    [SerializeField] private TextMeshProUGUI nameField;
    [SerializeField] private TextMeshProUGUI scoreField;


    public int Rank { get => rank; set => rank = value; }
    public string Name { get => _name; set => _name = value; }
    public int Score { get => score; set => score = value; }

    public void SetDataEntries(int rank, string name, int score)
    {
        this.rank = rank;
        this._name = name;
        this.score = score;

        rankField.text = this.rank.ToString();
        nameField.text = this._name.ToString();
        scoreField.text = this.score.ToString();
       
    }

}
