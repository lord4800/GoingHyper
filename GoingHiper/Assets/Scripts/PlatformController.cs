using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private const string MOVE_TRIGGER = "move";
    [SerializeField] Animator animator;

    void Start()
    {
        AnimatorCheck();
    }

    private void AnimatorCheck()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        animator.SetTrigger(MOVE_TRIGGER);
    }
}
