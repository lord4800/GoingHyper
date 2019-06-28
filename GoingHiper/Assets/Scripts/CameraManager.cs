﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static private CameraManager instance;
    static public CameraManager Instance { get { return instance; } }

    [SerializeField] private List<Transform> cameraPos = new List<Transform>();
    [SerializeField] private List<GameObject> TrapsManagers = new List<GameObject>();
    [SerializeField] private float toPosTime = 2f;

    private int cameraID;
    private Vector3 targetPos;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        cameraID = 0;
    }

    public void SwitchCamera()
    {
        TrapsManagers[cameraID].SetActive(false);
        targetPos = cameraPos[cameraID].position;
        StartCoroutine(ToNewPositionCoroutine());
        cameraID++;
        if (cameraID < TrapsManagers.Count)
        TrapsManagers[cameraID].SetActive(true);
    }

    IEnumerator ToNewPositionCoroutine()
    {
        for (float f = toPosTime; f > 0; f -= Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 1-f/toPosTime);
            yield return null;
        }
    }
}
