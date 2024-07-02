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
    

    private void Awake()
    {
        runner = GetComponent<LaneRunner>();
        speed = runner.followSpeed;
        runner.followSpeed = 0f;
        instance = this;
    }

    private void Start()
    {
        

        //StartCoroutine(Countdown()); 
        Debug.Log($"speed - {runner.followSpeed}");
    }

    private void Update()
    {
        if (LevelGenerator.instance.ready)
            runner.followSpeed = speed;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) runner.lane--;
        if (Input.GetKeyDown(KeyCode.RightArrow)) runner.lane++;

        if (Input.GetKeyDown(KeyCode.Space)) LevelGenerator.instance.Restart();       //Leaderboard.instance.SaveScoresToJSON();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(3f);
        runner.followSpeed = speed;
        Debug.Log("Player Start Moving");
    }

    public void SetSpeed(float speed)
    {
        if(this.speed <= 30 && this.speed > 0)
        {
            this.speed = speed;
            this.runner.followSpeed = speed;
        }
        Debug.Log($"speed - {runner.followSpeed}");
        if (this.speed == 0)
        {
            LevelGameManager.instance.OnGameOver();
            Debug.Log("GAME OVER");
        }
           

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
