using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    PlayerInputAction action;
    InputAction moveAction;
    PlayerAnimation playerAnimation;
    private float movementSpeed = 0.05f;
    private Transform cameraTransform;

    private void Awake()
    {
        action = new PlayerInputAction();
        moveAction = action.Player.Move;
        moveAction.Enable();
        moveAction.started += OnMoveStarted;
        moveAction.canceled += OnMoveCanceled;
        playerAnimation = GetComponent<PlayerAnimation>();
        cameraTransform = Camera.main.transform;
    }


    void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            // Debug.Log("Play WalkAnimation");
            playerAnimation.PlayWalkAnimation();
        }
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            // Debug.Log("Play IdleAnimation");
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
        Vector3 moveDirection = new Vector3(x * movementSpeed, 0, z * movementSpeed);
        // Rotate(moveDirection);
        transform.Translate(moveDirection * Time.deltaTime, Space.Self);
    }

    void Rotate(Vector3 moveDirection)
    {
        if (moveDirection != Vector3.zero)
        {
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
        }
    }

}
