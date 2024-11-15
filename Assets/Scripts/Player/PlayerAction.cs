using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    PlayerInputAction action;
    InputAction moveAction, fireAction;
    PlayerAnimation playerAnimation;
    private float movementSpeed = 20f;
    private Transform cameraTransform;
    private GameObject shootingPoint;

    private void Awake()
    {
        action = new PlayerInputAction();
        action.Player.Enable();
        moveAction = action.Player.Move;
        playerAnimation = GetComponent<PlayerAnimation>();
        moveAction.Enable();
        moveAction.started += OnMoveStarted;
        moveAction.canceled += OnMoveCanceled;

        fireAction = action.Player.Fire;
        fireAction.Enable();
        fireAction.performed += OnFirePerformed;
        cameraTransform = Camera.main.transform;

        shootingPoint = transform.Find("ShootingPoint").gameObject;
    }

    void Update()
    {
        Vector3 keyboardVector = moveAction.ReadValue<Vector3>();
        Move(keyboardVector.x, keyboardVector.z);
    }

    void OnDestroy()
    {
        action.Player.Disable();
    }

    void Move(float x, float z)
    {
        Vector3 moveDirection = new Vector3(x, 0, z);
        // Rotate(moveDirection);
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.Self);
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

    void OnFirePerformed(InputAction.CallbackContext context)
    {
        GameObject bulletGameObject = ObjectPoolManager.Instance.GetGameObject("Bullet");
        if (bulletGameObject != null && shootingPoint != null)
        {
            bulletGameObject.transform.position = shootingPoint.transform.position;
            bulletGameObject.transform.rotation = shootingPoint.transform.rotation;
        }
    }

    void Rotate(Vector3 moveDirection) // 카메라 회전은 PlayerAction이 담당하지 않음, 미사용 함수
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