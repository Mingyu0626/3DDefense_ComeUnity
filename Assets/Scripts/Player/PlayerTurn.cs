using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerTurn : MonoBehaviour
{
    PlayerInputAction action;
    InputAction turnAction;
    Camera mainCamera;
    private float turnSpeed = 0.06f;
    private float minXRotation = -15f;
    private float maxXRotation = 10f;
    private float curXRotation = 0f;

    private void Awake()
    {
        action = new PlayerInputAction();
        turnAction = action.Player.Turn;
        turnAction.Enable();
        mainCamera = transform.Find("Main Camera").GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Start()
    {
        
    }

    void Update()
    {
        Vector2 mouseDelta = turnAction.ReadValue<Vector2>();
        TurnYAxis(mouseDelta);
        TurnXAxis(mouseDelta);
    }

    void TurnYAxis(Vector2 delta)
    {
        transform.Rotate(0, delta.x * turnSpeed, 0);
    }

    void TurnXAxis(Vector2 delta)
    {
        curXRotation -= delta.y * turnSpeed;
        curXRotation = Mathf.Clamp(curXRotation, minXRotation, maxXRotation);
        mainCamera.transform.localEulerAngles = new Vector3(curXRotation, 0, 0);
    }
}
