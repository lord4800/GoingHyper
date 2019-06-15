using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private Vector3 fallVec;

    private Animator animator;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void ActivateTrap()
    {
        animator.Play("push_trap_anim");
    }

    public void PhysicOn()
    {
        rigidbody.isKinematic = false;
        StartCoroutine(WaitPhysics());
        //Add tonque
    }

    IEnumerator WaitPhysics()
    {
        yield return new WaitForFixedUpdate();
        rigidbody.AddForce(fallVec, ForceMode.Impulse);
    }
}
