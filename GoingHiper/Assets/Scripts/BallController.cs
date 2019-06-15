using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 moveVector;
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

    private void UpdatePosition()
    {
        if (move)
        {
            transform.position += moveVector * Time.deltaTime;
        }
    }
}
