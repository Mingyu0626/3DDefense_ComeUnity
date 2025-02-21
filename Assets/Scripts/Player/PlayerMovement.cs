using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController), typeof(PlayerDataModel))]
public class PlayerMovement : MonoBehaviour, IInputAction
{
    private PlayerController playerController;
    private PlayerDataModel playerDataModel;
    private InputAction moveAction;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerDataModel = GetComponent<PlayerDataModel>();
        moveAction = InputManager.Instance.Action.Player.Move;
    }
    private void Update()
    {
        Vector3 inputVector = moveAction.ReadValue<Vector3>();
        Move(inputVector);
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
        playerController.SetIsWalking(true);
    }

    public void OnInputActionCanceled(InputAction.CallbackContext context)
    {
        playerController.SetIsWalking(false);
    }
    private void Move(Vector3 inputVector)
    {
        transform.Translate(inputVector * playerDataModel.PlayerSpeed * Time.deltaTime, Space.Self);
    }
}
