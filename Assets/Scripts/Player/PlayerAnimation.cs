using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private enum AnimationStateName
    {
        Idle,
        Walk,
        Shoot,
        ShootWhileRunning
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
        }
    }
    public void SetIsShooting(bool val)
    {
        if (animator != null)
        {
            animator.SetBool(nameof(Parameter.isShooting), val);
        }
    }
}