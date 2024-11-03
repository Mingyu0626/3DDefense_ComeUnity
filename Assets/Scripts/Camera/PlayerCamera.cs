using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    PlayerInputAction action;
    InputAction turnAction;
    Camera mainCamera;

    public Transform playerTransform;

    private float turnSpeed = 0.2f;
    private float minXRotation = -10f;
    private float maxXRotation = 30f;
    private float XRotation = 0f;
    private float YRotation = 0f;
    private float rotationTime = 0.1f;
    private float distanceFromPlayer = 15f;

    private Vector3 targetRotation;
    private Vector3 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        action = new PlayerInputAction();
        turnAction = action.Player.Turn;
        turnAction.Enable();
        mainCamera = gameObject.GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = turnAction.ReadValue<Vector2>();
        TurnYAxis(mouseDelta);
        TurnXAxis(mouseDelta);

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(XRotation, YRotation),
            ref currentVelocity, rotationTime);
        transform.eulerAngles = targetRotation;
        transform.position = playerTransform.position - transform.forward * distanceFromPlayer;
        transform.position += Vector3.up * 8f;

        UpdatePlayerRotation();
    }

    void TurnYAxis(Vector2 delta)
    {
        YRotation += delta.x * turnSpeed;
    }

    void TurnXAxis(Vector2 delta)
    {
        XRotation -= delta.y * turnSpeed;
        XRotation = Mathf.Clamp(XRotation, minXRotation, maxXRotation);
    }

    void UpdatePlayerRotation()
    {
        Vector3 playerForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        if (playerForward != Vector3.zero)
        {
            playerTransform.forward = playerForward;
        }
    }
}
