using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAction : MonoBehaviour
{
    private InputAction turnAction;
    private Camera mainCamera;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform shootingPointTransform;

    private float turnSpeed;
    private float minXRotation = -5f;
    private float maxXRotation = 30f;
    private float xAxis = 0f;
    private float yAxis = 0f;

    private float rotationTime = 0.1f;
    private float distanceFromPlayer = 20f;
    private float yPositionCorrection = 8f;

    private Vector3 targetRotation;
    private Vector3 currentVelocity;

    private void Awake()
    {
        turnAction = InputManager.Instance.Action.Player.Turn;
        mainCamera = gameObject.GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector2 mouseDelta = turnAction.ReadValue<Vector2>();
        Turn(mouseDelta);
        UpdateCameraTransform();
        UpdatePlayerRotation();
        UpdateShootingPointRotation();
    }
    void Turn(Vector2 delta)
    {
        turnSpeed = SavedSettingData.MouseSensitivity;
        yAxis += delta.x * turnSpeed * 0.01f;
        xAxis -= delta.y * SavedSettingData.MouseSensitivity * 0.01f;
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
        // cameraForward.y Clamp�ϴ°� ��������
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