using Dreamteck.Forever;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OptionsContainer :Builder
{
    [SerializeField] private float m_BoostAmount = 2f;
    [SerializeField] private float m_retardationAmount = 4f;
    private float speed;
    private OptionTab[] _boostPoints;
    [SerializeField] private Material BaseMat;
    [SerializeField] private Material RightOptionMat;
    [SerializeField] private Material WrongOptionMat;
    [SerializeField] private int CorrectOptionPoints = 5;
    [SerializeField] private int WrongOptionPoints = 2;
    public bool answered = false;
    [SerializeField] private int _minOperandA = 1;
    [SerializeField] private int _maxOperandA = 30;
    [SerializeField] private int _minOperandB = 0;
    [SerializeField] private int _maxOperandB = 20;
    [SerializeField] private TextMeshProUGUI questionText;
    Transform cameraTrs;
    float alpha = 0f;
    float lastAlpha = 0f;
    public float displayDistance = 20f;

    [SerializeField] private AudioClip correctSFX;
    [SerializeField] private AudioClip incorrectSFX;


    protected override void Build()
    {
        base.Build();
        cameraTrs = Camera.main.transform;
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
            _boostPoints[i].ToggleCollider(true);
            _boostPoints[i].SetAlpha(0f);
        }
        SetTextAlpha(questionText, 0f);
        Debug.Log("Build DONE");
    }

    private void Update()
    {
        if (!isDone) return;
        if (!answered)
        {
            //Make the texts appear if the question is not answered
            if (Vector3.Distance(trs.position, cameraTrs.position) <= displayDistance) 
                alpha = Mathf.MoveTowards(alpha, 1f, Time.deltaTime * PlayerController.instance.GetSpeed() * 0.1f);
            else 
                alpha = Mathf.MoveTowards(alpha, 0f, Time.deltaTime);
        }
        else
        {
            alpha = Mathf.MoveTowards(alpha, 0f, Time.deltaTime * 0.1f);
        }
        if (alpha != lastAlpha)
        {
            SetTextAlpha(questionText, alpha);
            for (int i = 0; i < _boostPoints.Length; i++) _boostPoints[i].SetAlpha(alpha);
            lastAlpha = alpha;
        }
    }

     private void SetTextAlpha(TextMeshProUGUI tm, float alpha)
    {
        Color col = tm.color;
        col.a = alpha;
        tm.color = col;
    }


    public void OnRightOptionHit()
    {
        PlayerHealthManager.instance.CancelStreak();
        foreach (OptionTab point  in _boostPoints)
        {
            if (point.IsCorrect)
                point.SetMaterial(RightOptionMat);
            point.ToggleCollider(false);
        }
        answered = true;
        ScoreManager.instance.AddScore(CorrectOptionPoints);
        PlayerController.instance.SetSpeed(PlayerController.instance.GetSpeed() + m_BoostAmount);
        AudioManager.instance.PlaySFX(correctSFX);
    }

    public void OnWrongOptionHit()
    {
        PlayerHealthManager.instance.MakeStreak();
        foreach (OptionTab point in _boostPoints)
        {
            if (point.IsCorrect)
                point.SetMaterial(RightOptionMat);
            else
                point.SetMaterial(WrongOptionMat);
            point.ToggleCollider(false);
        }
        answered = true;
        ScoreManager.instance.ReduceScore(WrongOptionPoints);
        PlayerController.instance.SetSpeed(PlayerController.instance.GetSpeed() - m_retardationAmount);
        AudioManager.instance.PlaySFX(incorrectSFX);
    }
}
