using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
    }

    public void ActivateTrap()
    {
        animator.Play("");
    }
}
