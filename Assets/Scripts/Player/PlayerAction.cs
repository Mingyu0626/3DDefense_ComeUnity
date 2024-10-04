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
        moveAction.started += OnMoveStarted;
        moveAction.canceled += OnMoveCanceled;
    }


    void OnMoveStarted(InputAction.CallbackContext context)
    {
        playerAnimation.PlayWalkAnimation(); // 걷는 애니메이션 시작
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        playerAnimation.PlayIdleAnimation(); // 서있는 애니메이션 시작
    }


    void Start()
    {
        moveAction.Enable();
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
