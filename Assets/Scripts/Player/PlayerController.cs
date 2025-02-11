using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private enum PlayerStateName
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