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
        Backward,
        Finish,
        SwitchScreen
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
    private const float SPEED = 5f;

    [SerializeField] private float TURNOFF_Y = 0f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private Animator animator;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material blackMat;
    [SerializeField] public ColorType colorType = ColorType.Yellow;
    [SerializeField] public BallController forwardBall;
    [SerializeField] public BallController backwardBall;

    [SerializeField] private SwitchNode currentGoal;

    public BallLineController ballLineController;

    private bool move;
    public bool checkDistance;
    private Transform targetBall;
    private Vector3 currentMoveVector;
    private VectorType currentVectorType;

    public SwitchNode Goal { get { return currentGoal; } }

    public bool Wait { get { return !move; } }

    // Start is called before the first frame update
    void Start()
    {
        StartMove();
        currentMoveVector = new Vector3(0, 0, 1);
        currentVectorType = VectorType.Forward;
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
    public void UpdateBall(BallController nextBall = null, BallController previousBall = null)
    {
        if (!isActiveAndEnabled) return;

        UpdatePosition(nextBall, previousBall);

        if (CheckTakeGoal())
        {
            Rotate(currentGoal);
        }
    }

    private bool CheckTakeGoal()
    {
        switch (currentVectorType)
        {
            case VectorType.Forward:
                return transform.position.z > currentGoal.GoalPos.z;
                break;
            case VectorType.Backward:
                return transform.position.z < currentGoal.GoalPos.z;
                break;
            case VectorType.Right:
                return transform.position.x > currentGoal.GoalPos.x;
                break;
            case VectorType.Left:
                return transform.position.x < currentGoal.GoalPos.x;
                break;
        }
        return false;
    }

    public void StopMove()
    {
        move = false;
    }

    public void Rotate(SwitchNode rotate)
    {
        //RotateVector
        Vector3 temp = moveVector;
        transform.position = rotate.GoalPos;
        switch (rotate.VectorType)
        {
            case VectorType.Finish: Finish(); break;
            case VectorType.SwitchScreen: rotate.SwitchCam(); UpdateGoal(rotate);
                currentVectorType = VectorType.Forward;
                break;
            case VectorType.Backward:
            case VectorType.Forward:
            case VectorType.Left:
            case VectorType.Right:
                currentVectorType = rotate.VectorType;
                UpdateGoal(rotate);
                break;
        }
    }

    private void UpdateGoal(SwitchNode rotate)
    {
        currentMoveVector = (rotate.NextGoal.GoalPos - rotate.GoalPos).normalized;
        currentGoal = rotate.NextGoal;
    }

    public void FallDownEvent()
    {
        gameObject.SetActive(false);
        if (FallDown != null)
            FallDown();
    }

    private void OnTriggerEnter(Collider other)
    {
        DropDown(other);
        /*
        switch (other.tag)
        {
            case "Rotate": Rotate(other.GetComponent<SwitchNode>()); break;
            default: DropDown(other); break;
        }*/
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
            ballLineController.Death(this);
            //ballLineController.StopPartLine(this);
            if (currentMoveVector.z == 0)
                animator.Play("ball_fall_horizontal");
            else
                animator.Play("ball_fall");
        }
    }

    private void UpdatePosition(BallController nextBall = null, BallController previousBall = null)
    {
        if (move)
        {
            //if (previousBall != null && previousBall.Goal == currentGoal && Vector3.Distance(previousBall.transform.position, transform.position) > 1.4f)
            //    return;
            //else
            if (nextBall != null && nextBall.Goal == currentGoal && Vector3.Distance(nextBall.transform.position, transform.position) < 1.5f)
            {
                switch (currentVectorType)
                {
                    case VectorType.Forward:
                        transform.position = nextBall.transform.position - Vector3.forward * 1.25f;
                        break;
                    case VectorType.Backward:
                        transform.position = nextBall.transform.position - Vector3.back * 1.25f;
                        break;
                    case VectorType.Right:
                        transform.position = nextBall.transform.position - Vector3.right * 1.25f;
                        break;
                    case VectorType.Left:
                        transform.position = nextBall.transform.position - Vector3.left * 1.25f;
                        break;
                }
            }
            else
            {
                transform.position += currentMoveVector * SPEED * Time.deltaTime;
            }
        }
    }
}
