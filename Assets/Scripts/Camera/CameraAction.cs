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
    [SerializeField]
    private Transform shootingPointTransform;

    private float turnSpeed = 0.1f;
    private float minXRotation = -5f;
    private float maxXRotation = 30f;
    private float xAxis = 0f;
    private float yAxis = 0f;

    private float rotationTime = 0.1f;
    private float distanceFromPlayer = 20f;
    private float yPositionCorrection = 8f;

    private Vector3 targetRotation;
    private Vector3 currentVelocity;

    void Start()
    {
        action = new PlayerInputAction();
        turnAction = action.Player.Turn;
        turnAction.Enable();
        mainCamera = gameObject.GetComponent<Camera>();
        GameManager.Instance.SetCursorUseable(false);
    }
    void Update()
    {
    }

    void LateUpdate()
    {
        Vector2 mouseDelta = turnAction.ReadValue<Vector2>();
        Turn(mouseDelta);
        UpdateCameraTransform();
        UpdatePlayerRotation();
        UpdateShootingPointRotation();
    }
    void OnDestroy()
    {
        action.Player.Disable();
    }

    void Turn(Vector2 delta)
    {
        yAxis += delta.x * turnSpeed;
        xAxis -= delta.y * turnSpeed;
        xAxis = Mathf.Clamp(xAxis, minXRotation, maxXRotation);
        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(xAxis, yAxis), ref currentVelocity, rotationTime);
        transform.eulerAngles = targetRotation;
    }

    void UpdateCameraTransform()
    {
        transform.position = playerTransform.position - transform.forward * distanceFromPlayer;
        transform.position += Vector3.up * yPositionCorrection;
    }

    void UpdatePlayerRotation()
    {
        Vector3 cameraForward = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        if (cameraForward != Vector3.zero)
        {
            playerTransform.forward = cameraForward;
        }
    }

    void UpdateShootingPointRotation()
    {
        Vector3 cameraForward = transform.forward.normalized;
        // cameraForward.y Clamp하는게 나으려나
        if (cameraForward.y < -0.1f)
        {
            cameraForward.y = -0.1f;
        }
        if (cameraForward != Vector3.zero)
        {
            shootingPointTransform.forward = cameraForward;
        }
    }
}
