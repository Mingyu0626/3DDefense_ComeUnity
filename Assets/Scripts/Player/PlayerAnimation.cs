using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo stateInfo; 
    private enum AnimationName
    {
        Idle_gunMiddle_AR,
        Run_guard_AR,
        Shoot_Autoshot_AR,
        Run_gunMiddle_AR,
        SingleShot_ARShot_AR
    }
    private enum Parameter
    {
        isWalking,
        isShooting
    }
    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("Animator°¡ null");
        }
    }
    public void SetIsWalking(bool val)
    {
        if (animator != null)
        {
            animator.SetBool(nameof(Parameter.isWalking), val);
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
    public void SetIsShooting(bool val)
    {
        if (animator != null)
        {
            animator.SetBool(nameof(Parameter.isShooting), val);
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}