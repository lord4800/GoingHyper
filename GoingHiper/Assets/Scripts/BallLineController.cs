using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLineController : MonoBehaviour
{
    [SerializeField] private List<BallController> ballsLine = new List<BallController>();
    public Action WinAction;
    private void Awake()
    {
        foreach (var ball in ballsLine)
        {
            ball.FallDown += OnBallFall;
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
