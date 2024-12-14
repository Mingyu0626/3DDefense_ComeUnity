using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSFX))]
public class PlayerAction : MonoBehaviour
{
    InputActions action;
    InputAction moveAction, fireAction;
    PlayerAnimation playerAnimation;
    private float movementSpeed = 20f;
    private Transform cameraTransform;
    [SerializeField]
    private Transform shootingPoint;
    [SerializeField]
    private ParticleSystem fireEffect;
    private AudioSFX audioSfx;
    private AudioSource fireAudio;

    private void Awake()
    {
        action = new InputActions();
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

        audioSfx = GetComponent<AudioSFX>();
        fireAudio = audioSfx.AudioSourceSfx;
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

    void OnMoveStarted(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (playerAnimation is not null)
        {
            // Debug.Log("Play WalkAnimation");
            playerAnimation.PlayWalkAnimation();
        }
    }

    void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (playerAnimation is not null)
        {
            // Debug.Log("Play IdleAnimation");
            playerAnimation.PlayIdleAnimation();
        }
    }

    void OnFirePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        GameObject playerBulletGO = ObjectPoolManager.Instance.GetGameObject("Bullet");
        if (playerBulletGO is not null && shootingPoint is not null)
        {
            playerBulletGO.transform.position = shootingPoint.position;
            playerBulletGO.transform.rotation = shootingPoint.rotation;
        }

        if (fireEffect is not null)
        {
            fireEffect.Play();
        }

        if (fireAudio is not null)
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