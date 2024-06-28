using Dreamteck.Forever;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OptionsContainer :Builder
{
    [SerializeField] private float m_speedBoostAmount = 2f;
    private float speed;
    private OptionTab[] _boostPoints;
    [SerializeField] private Material BaseMat;
    [SerializeField] private Material RightOptionMat;
    [SerializeField] private Material WrongOptionMat;
    [SerializeField] private int CorrectOptionPoints = 5;
    [SerializeField] private int WrongOptionPoints = 2;

    [SerializeField] private int _minOperandA = 1;
    [SerializeField] private int _maxOperandA = 30;
    [SerializeField] private int _minOperandB = 0;
    [SerializeField] private int _maxOperandB = 20;
    [SerializeField] private TextMeshProUGUI questionText;


    protected override void Build()
    {
        base.Build();

        _boostPoints = GetComponentsInChildren<OptionTab>();
        int a = Random(_minOperandA, _maxOperandA + 1);
        int b = Random(_minOperandB, _maxOperandB + 1);
        int operation = Random(0, 3);
        string[] operations = new string[] { "+", "-", "*" };
        questionText.text = a + " " + operations[operation] + " " + b;
        float answer = 0f;
        switch (operation)
        {
            case 0: answer = a + b; break;
            case 1: answer = a - b; break;
            case 2: answer = a * b; break;
        }
        answer = Mathf.Round(answer * 10f) / 10f;

        int CorrectIndex = UnityEngine.Random.Range(0, _boostPoints.Count());
        for(int i = 0; i< _boostPoints.Count(); i++)
        {
            _boostPoints[i].container = this;
            if (CorrectIndex == i) _boostPoints[i].Initialize(true, answer.ToString());
            else
            {
                int randomAnswer = Random(-20, 99);
                if (randomAnswer == answer) randomAnswer += Random(1, 11) * (Random(0, 2) == 0 ? -1 : 1);
                _boostPoints[i].Initialize(false, randomAnswer.ToString());
            }
            _boostPoints[i].GetComponent<MeshRenderer>().sharedMaterial = BaseMat;
        }
        Debug.Log("Build DONE");
    }



    //private void Start()
    //{
    //    foreach (OptionTab point in _boostPoints)
    //    {
    //        point.container = this;
    //        point.GetComponent<MeshRenderer>().sharedMaterial = BaseMat;
    //    }
    //}

    public void OnRightOptionHit()
    {
        foreach (OptionTab point  in _boostPoints)
        {
            if (point.IsCorrect)
                point.SetMaterial(RightOptionMat);
        }
        PlayerController.instance.SetSpeed(PlayerController.instance.GetSpeed() + m_speedBoostAmount);
        ScoreManager.instance.AddScore(CorrectOptionPoints);
    }

    public void OnWrongOptionHit()
    {
        foreach (OptionTab point in _boostPoints)
        {
            if (point.IsCorrect)
                point.SetMaterial(RightOptionMat);
            else
                point.SetMaterial(WrongOptionMat);
        }
        PlayerController.instance.SetSpeed(PlayerController.instance.GetSpeed() - m_speedBoostAmount);
        ScoreManager.instance.ReduceScore(WrongOptionPoints);
    }
}
