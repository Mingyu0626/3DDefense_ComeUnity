using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    PlayerInputAction action;
    InputAction moveAction;
    PlayerAnimation playerAnimation;

    private void Awake()
    {
        action = new PlayerInputAction();
        moveAction = action.Player.Move;
        playerAnimation = GetComponent<PlayerAnimation>();
        moveAction.Enable();
        moveAction.started += OnMoveStarted;
        moveAction.canceled += OnMoveCanceled;
    }


    void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            Debug.Log("Play WalkAnimation");
            playerAnimation.PlayWalkAnimation();
        }
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            Debug.Log("Play IdleAnimation");
            playerAnimation.PlayIdleAnimation();
        }
    }


    void Start()
    { 
    }

    void Update()
    {
        Vector3 keyboardVector = moveAction.ReadValue<Vector3>();
        Move(keyboardVector.x, keyboardVector.z);
    }

    void Move(float x, float z)
    {
        this.transform.position = new Vector3(
                this.transform.position.x + x, 
                this.transform.position.y, 
                this.transform.position.z + z);
    }
}
