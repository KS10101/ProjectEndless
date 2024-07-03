using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    LaneRunner runner;
    float speed;
    GameObject lvlmanager;
    [SerializeField] Animator PlayerAnimator;
    [SerializeField] GameObject CountdownCanvas;

    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip countdownClip;

    private void Awake()
    {
        runner = GetComponent<LaneRunner>();
        speed = runner.followSpeed;
        runner.followSpeed = 0f;
        instance = this;

    }

    private void Start()
    {
        StartCoroutine(Countdown());
        Debug.Log($"speed - {runner.followSpeed}");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) runner.lane--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) runner.lane++;

        if (Input.GetKeyDown(KeyCode.Space)) LevelGenerator.instance.Restart();
        Leaderboard.instance.SaveScoresToJSON();

        Debug.Log($"animation Speed: {PlayerAnimator.GetFloat("SpeedRate")}");
    }

    private IEnumerator Countdown()
    {
        CountdownCanvas.SetActive(true);
        CountdownCanvas.GetComponent<Animator>().enabled = true;
        AudioManager.instance.PlaySFX(countdownClip);
        yield return new WaitForSeconds(4f);
        if (LevelGenerator.instance.ready)
            runner.followSpeed = speed;
        Debug.Log("Player Start Moving");
        SetAniamtionSpeed(1);
        CountdownCanvas.GetComponent<Animator>().enabled = false;
        CountdownCanvas.SetActive(false);
        AudioManager.instance.PlayBGSound(backgroundClip);
    }

    public void SetSpeed(float speed)
    {
        if(this.speed <= 30 && this.speed > 0)
        {
            this.speed = speed;
            this.runner.followSpeed = speed;
        }
        Debug.Log($"speed : {runner.followSpeed}");
        if (this.speed <= 2)
        {
            this.speed = 0;
            this.runner.followSpeed = this.speed;
            lvlmanager = FindAnyObjectByType<LevelGameManager>().gameObject;
            lvlmanager.GetComponent<LevelGameManager>().enabled = true;
            Debug.Log("GAME OVER");
        }
        SetAniamtionSpeed(this.runner.followSpeed / 10);
    }

    private void SetAniamtionSpeed(float rate)
    {
        PlayerAnimator.SetFloat("SpeedRate", rate);
    }

    public float GetSpeed()
    {
        return speed;
    }

    private void EvaluateSpline()
    {
        SplineSample evalResult = new SplineSample();
        LevelSegment segment = LevelGenerator.instance.segments[0];
        segment.Evaluate(0.5, ref evalResult);
        Debug.Log($"Spline \n Postion: {evalResult.position}" +
            $" Size: {evalResult.size} " +
            $"Percent: {evalResult.percent}");
    }
}
