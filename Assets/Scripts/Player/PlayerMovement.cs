using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IInputAction
{
    private InputAction moveAction;
    private PlayerAnimation playerAnimation;
    private float speed = 20f;

    private void Awake()
    {
        moveAction = InputManager.Instance.Action.Player.Move;
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        Vector3 inputVector = moveAction.ReadValue<Vector3>();
        Move(inputVector);
    }
    private void Move(Vector3 inputVector)
    {
        transform.Translate(inputVector * speed * Time.deltaTime, Space.Self);
    }

    public void AddInputActionEvent()
    {
        RemoveInputActionEvent();
        moveAction.started += OnInputActionStarted;
        moveAction.canceled += OnInputActionCanceled;
    }

    public void RemoveInputActionEvent()
    {
        moveAction.started -= OnInputActionStarted;
        moveAction.canceled -= OnInputActionCanceled;
    }

    public void OnInputActionStarted(InputAction.CallbackContext context)
    {
        playerAnimation.SetIsWalking(true);
    }

    public void OnInputActionCanceled(InputAction.CallbackContext context)
    {
        playerAnimation.SetIsWalking(false);
    }
}
