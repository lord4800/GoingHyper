using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public enum VectorType
    {
        Left,
        Right,
        Forward,
        Backward
    }

    [SerializeField] private float TURNOFF_Y = 0f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private Animator animator;
    private bool move;
    private Vector3 currentMoveVector;

    public Action FallDown;
    public Action GameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        move = true;
        Rotate(VectorType.Forward);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void Rotate(VectorType vectorType)
    {
        //RotateVector
        Vector3 temp = moveVector;
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(transform.position.x);
        pos.z = Mathf.Round(transform.position.z);
        transform.position = pos;
        switch (vectorType)
        {
            case VectorType.Forward: { currentMoveVector = new Vector3(0, 0, temp.z); break; }
            case VectorType.Left: { currentMoveVector = new Vector3(-temp.z, 0, 0); break; }
            case VectorType.Right: { currentMoveVector = new Vector3(temp.z, 0, 0); break; }
            case VectorType.Backward: { currentMoveVector = new Vector3(0, 0, -temp.z); break; }
        }
    }

    public void FallDownEvent()
    {
        gameObject.SetActive(false);
        if (FallDown != null)
            FallDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (GameOverEvent != null)
                GameOverEvent();
            MessageController.Instance.GameOverEvent();
        } else if (other.tag == "SwitchCamera")
            {
            //nothing , maybe stop
        }
        else if (other.tag != "Rotate")
        {
            move = false;
            if (currentMoveVector.z == 0)
                animator.Play("ball_fall_horizontal");
            else
                animator.Play("ball_fall");
        }
        else
        {
            Rotate(other.GetComponent<Rotate>().VectorType);
        }
    }

    private void UpdatePosition()
    {
        if (move)
        {
            transform.position += currentMoveVector * Time.deltaTime;
        }
    }
}
