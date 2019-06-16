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

    [SerializeField] private float TURNOFF_Y = 0f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private Animator animator;
    [SerializeField] private Material yellowMat;
    [SerializeField] private Material blackMat;
    [SerializeField] private ColorType colorType = ColorType.Yellow;

    private bool move;
    private Vector3 currentMoveVector;

    public Action FallDown;
    public Action GameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        move = true;
        Rotate(VectorType.Forward);
        if (colorType == ColorType.Black)
            GetComponent<Renderer>().material = blackMat;
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
            move = false;
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
