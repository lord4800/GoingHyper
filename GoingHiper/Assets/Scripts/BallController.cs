using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float TURNOFF_Y = 0f;
    [SerializeField] private Vector3 moveVector;
    [SerializeField] private Animator animator;
    private bool move;

    // Start is called before the first frame update
    void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    public void Rotate(bool isLeft)
    {
        //RotateVector
    }

    public void FallEffect()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        move = false;
        animator.Play("ball_fall");
    }

    private void UpdatePosition()
    {
        if (move)
        {
            transform.position += moveVector * Time.deltaTime;
        }
    }
}
