using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private List<IInputAction> interfaceInputActionList;
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
        interfaceInputActionList = new List<IInputAction>();
        interfaceInputActionList.Add(GetComponent<PlayerMovement>());
        interfaceInputActionList.Add(GetComponent<PlayerFire>());
        animator = transform.GetComponent<Animator>();
        AddActionEvent();
    }
    private void OnDestroy()
    {
        RemoveActionEvent();
    }
    public void AddActionEvent()
    {
        for (int i = 0; i < interfaceInputActionList.Count; i++)
        {
            interfaceInputActionList[i].AddInputActionEvent();
        }
    }
    public void RemoveActionEvent()
    {
        for (int i = 0; i < interfaceInputActionList.Count; i++)
        {
            interfaceInputActionList[i].RemoveInputActionEvent();
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