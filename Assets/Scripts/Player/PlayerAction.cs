using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSFX))]
public class PlayerAction : MonoBehaviour
{
    private InputAction moveAction, fireAction;
    private PlayerAnimation playerAnimation;
    private float movementSpeed = 20f;
    private Transform cameraTransform;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem fireEffect;

    private AudioSFX audioSfx;
    private AudioSource fireAudio;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        moveAction = InputManager.Instance.Action.Player.Move;
        fireAction = InputManager.Instance.Action.Player.Fire;

        playerAnimation = GetComponent<PlayerAnimation>();
        AddMoveActionEvent();
        AddFireActionEvent();

        audioSfx = GetComponent<AudioSFX>();
        fireAudio = audioSfx.AudioSourceSfx;
    }

    private void Update()
    {
        Vector3 keyboardVector = moveAction.ReadValue<Vector3>();
        Move(keyboardVector.x, keyboardVector.z);
    }
    private void OnDestroy()
    {
        moveAction.started -= OnMoveStarted;
        moveAction.canceled -= OnMoveCanceled;
        fireAction.performed -= OnFirePerformed;
    }
    void Move(float x, float z)
    {
        Vector3 moveDirection = new Vector3(x, 0, z);
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.Self);
    }
    void AddMoveActionEvent()
    {
        if (moveAction != null && playerAnimation != null)
        {
            moveAction.started -= OnMoveStarted;
            moveAction.started += OnMoveStarted;

            moveAction.canceled -= OnMoveCanceled;
            moveAction.canceled += OnMoveCanceled;
        }
    }
    void AddFireActionEvent()
    {
        if (fireAction != null && playerAnimation != null)
        {
            Debug.Log("FireAction에 이벤트 추가");
            fireAction.performed -= OnFirePerformed;
            fireAction.performed += OnFirePerformed;
        }
    }
    void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            playerAnimation.PlayWalkAnimation();
        }
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            playerAnimation.PlayIdleAnimation();
        }
    }

    void OnFirePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("OnFirePerformed 실행");
        GameObject playerBulletGO = ObjectPoolManager.Instance.GetGameObject("Bullet");
        if (playerBulletGO != null && shootingPoint != null)
        {
            playerBulletGO.transform.position = shootingPoint.position;
            playerBulletGO.transform.rotation = shootingPoint.rotation;
        }

        if (fireEffect != null)
        {
            fireEffect.Play();
        }

        if (fireAudio != null)
        {
            fireAudio.Play();
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