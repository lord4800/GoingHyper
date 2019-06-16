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

    public enum ColorType
    {
        Yellow,
        Black
    }

    public Action FallDown;
    public Action GameOverEvent;

    [SerializeField] private float TURNOFF_Y = 0f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private Animator animator;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material blackMat;
    [SerializeField] public ColorType colorType = ColorType.Yellow;
    [SerializeField] private float distanceBetween;
    [SerializeField] public BallController forwardBall;
    [SerializeField] public BallController backwardBall;

    public BallLineController ballLineController;

    private bool move;
    public bool checkDistance;
    private Transform targetBall;
    private Vector3 currentMoveVector;

    public bool Wait { get { return !move; } }

    // Start is called before the first frame update
    void Start()
    {
        StartMove();
        Rotate(VectorType.Forward);
        if (colorType == ColorType.Black)
            GetComponent<Renderer>().material = blackMat;
    }

    public void StartMove()
    {
        move = true;
        checkDistance = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        if (checkDistance)
        {
            if (Vector3.Distance(transform.position, forwardBall.transform.position) < distanceBetween)
            {
                forwardBall.StartMove();
            }
        }
    }

    public void StopMove()
    {
        move = false;
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
        switch (other.tag)
        {
            case "Finish": Finish(); break;
            case "SwitchCamera": break;
            case "Rotate": Rotate(other.GetComponent<Rotate>().VectorType); break;
            default: DropDown(other); break;
        }
    }

    private void Finish()
    {
        if (GameOverEvent != null)
            GameOverEvent();
        MessageController.Instance.GameOverEvent();
    }

    private void DropDown(Collider other)
    {
        if (colorType == other.GetComponent<TrapController>().ColorType)
        {
            StopMove();
            //ballLineController.StopPartLine(this);
            if (currentMoveVector.z == 0)
                animator.Play("ball_fall_horizontal");
            else
                animator.Play("ball_fall");
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
