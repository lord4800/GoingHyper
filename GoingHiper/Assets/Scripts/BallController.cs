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

    private const float MAX_Z = 120;
    private const float MIN_Z = -50;
    private const float MAX_X = 20;
    private const float MIN_X = -40;

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
    }

    public void PaintBall()
    {
        GetComponent<Renderer>().material = colorType == ColorType.Black ? blackMat : yellowMat;
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
        ballLineController.Stop();
    }

    private void DropDown(Collider other)
    {
        if (colorType == other.GetComponent<TrapController>().ColorType)
        {
            StopMove();
            ballLineController.Death();
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
            if (transform.position.x > MAX_X || transform.position.x < MIN_X || transform.position.z > MAX_Z || transform.position.z < MIN_Z)
            {
                StopMove();
                FallDownEvent();
            }
        }
    }
}
