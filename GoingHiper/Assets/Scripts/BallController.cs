using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Vector2 moveVector;
    public Action WinAction;


    void Start()
    {
        RicidbodyCheck();
    }

    private void RicidbodyCheck()
    {
        if (rigidbody2D == null)
            rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(false);
        rigidbody2D.simulated = false;
        if (WinAction != null)
            WinAction();
    }

    void OnMouseDown()
    {
        rigidbody2D.AddForce(moveVector, ForceMode2D.Impulse);
    }
}
