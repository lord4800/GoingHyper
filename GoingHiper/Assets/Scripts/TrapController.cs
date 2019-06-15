using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private Vector3 fallVec;

    private Animator animator;
    private Rigidbody rigidbody;
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void ActivateTrap()
    {
        animator.Play("push_trap_anim");
    }

    public void FallDownEvent()
    {
        gameObject.SetActive(false);
    }
}
