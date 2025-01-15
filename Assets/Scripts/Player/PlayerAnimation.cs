using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
    }

    void Start()
    {
        if (animator == null) Debug.Log("Animator°¡ null");
    }

    void Update()
    {
    }

    public void PlayWalkAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isShooting", false);
        }
    }

    public void PlayIdleAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isShooting", false);
        }
    }

    public void PlayShootAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isShooting", true);
        }
    }


}
