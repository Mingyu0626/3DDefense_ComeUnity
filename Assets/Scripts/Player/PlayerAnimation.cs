using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = transform.Find("PBRCharacter").GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayWalkAnimation()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isShooting", false);
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isShooting", false);
    }

    public void PlayShootAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isShooting", true);

    }


}
