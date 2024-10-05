using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerTurn : MonoBehaviour
{
    PlayerInputAction action;
    InputAction turnAction;
    PlayerAnimation playerAnimation;
    private float turnSpeed = 0.1f;

    private void Awake()
    {
        action = new PlayerInputAction();
        turnAction = action.Player.Turn;
        turnAction.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mouseDelta = turnAction.ReadValue<Vector2>();
        transform.Rotate(0, mouseDelta.x * turnSpeed, 0);
    }
}
