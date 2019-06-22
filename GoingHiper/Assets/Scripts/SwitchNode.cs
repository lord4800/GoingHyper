﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchNode : MonoBehaviour
{
    public BallController.VectorType VectorType;
    public SwitchNode NextGoal;
    public Vector3 GoalPos;

    private void Start()
    {
        GoalPos = transform.position + Vector3.up * 11;
    }

    public void SwitchCam()
    {
        CameraManager.Instance.SwitchCamera();
    }
}
