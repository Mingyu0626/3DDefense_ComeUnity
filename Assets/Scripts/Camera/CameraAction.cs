using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAction : MonoBehaviour
{
    PlayerInputAction action;
    InputAction turnAction;
    Camera mainCamera;

    [SerializeField]
    private Transform playerTransform;

    private float turnSpeed = 0.2f;
    private float minXRotation = -10f;
    private float maxXRotation = 30f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    private float rotationTime = 0.1f;
    private float distanceFromPlayer = 15f;
    private float yPositionCorrection = 8f;

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

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(xRotation, yRotation),
            ref currentVelocity, rotationTime);
        transform.eulerAngles = targetRotation;
        transform.position = playerTransform.position - transform.forward * distanceFromPlayer;
        transform.position += Vector3.up * yPositionCorrection;

        UpdatePlayerRotation();
    }

    void TurnYAxis(Vector2 delta)
    {
        yRotation += delta.x * turnSpeed;
    }

    void TurnXAxis(Vector2 delta)
    {
        xRotation -= delta.y * turnSpeed;
        xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
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
