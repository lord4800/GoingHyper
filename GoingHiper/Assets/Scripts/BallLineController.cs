using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLineController : MonoBehaviour
{
    public Action WinAction;

    [SerializeField] private List<BallController> ballsLine = new List<BallController>();

    [SerializeField] private List<BallController> waitBalls;
    
    private void Awake()
    {
        for (int i = 0; i < ballsLine.Count; i++)
        {
            BallController ball = ballsLine[i];
            ball.ballLineController = this;
            if (i - 1 >= 0)
                ball.forwardBall = ballsLine[i - 1];
            ball.FallDown += OnBallFall;
        }
        for (int i = 0; i < ballsLine.Count; i++)
        {
            BallController ball = ballsLine[i];
            if (i + 1 < ballsLine.Count)
                ball.backwardBall = ballsLine[i + 1];
        }
    }

    public void StopPartLine(BallController lastBall)
    {
        ballsLine.Remove(lastBall);
        int lastBallID = ballsLine.IndexOf(lastBall);
        if (lastBallID != ballsLine.Count - 1)
            for (int i = 0; i < ballsLine.Count; i++)
            {
                if (i < lastBallID && !ballsLine[i].Wait)
                {
                    ballsLine[i].StopMove();
                }
            }
        for (int i = 1; i < ballsLine.Count; i++)
        {
            ballsLine[i].forwardBall = ballsLine[i - 1];
            ballsLine[i].checkDistance = true;
        }
        /*
         * waitBalls = new List<BallController>();
        int lastBallID = ballsLine.IndexOf(lastBall);
        Debug.Log("lastBallID " + lastBallID);
        if (lastBallID != ballsLine.Count-1)
        for (int i = 0; i < ballsLine.Count; i++)
        {
            if (i < lastBallID && !ballsLine[i].Wait)
            {
                waitBalls.Add(ballsLine[i]);
                ballsLine[i].StopMove();
            }
        }
        */
    }

    public void StartPartLine()
    {
        foreach (var ball in waitBalls)
        {
            ball.StartMove();
        }
    }

    private void OnBallFall()
    {
        foreach (var ball in ballsLine)
        {
            if (ball.gameObject.activeInHierarchy) { return; }
        }

        if (WinAction != null)
        {
            WinAction();
        }
        MessageController.Instance.GameWinEvent();
    }

}
