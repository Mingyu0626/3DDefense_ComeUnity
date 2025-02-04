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
    private float roundsPerMinutes = 120f;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem fireEffect;

    private AudioSFX audioSfx;
    private AudioSource fireAudio;

    private void Awake()
    {
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
        fireAction.started -= OnFireStarted;
        fireAction.canceled -= OnFireCanceled;
    }
    private void Move(float x, float z)
    {
        Vector3 moveDirection = new Vector3(x, 0, z);
        transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.Self);
    }
    private void AddMoveActionEvent()
    {
        if (moveAction != null && playerAnimation != null)
        {
            moveAction.started -= OnMoveStarted;
            moveAction.started += OnMoveStarted;

            moveAction.canceled -= OnMoveCanceled;
            moveAction.canceled += OnMoveCanceled;
        }
    }
    private void AddFireActionEvent()
    {
        if (fireAction != null && playerAnimation != null)
        {
            fireAction.started -= OnFireStarted;
            fireAction.started += OnFireStarted;

            fireAction.canceled -= OnFireCanceled;
            fireAction.canceled += OnFireCanceled;
        }
    }
    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            playerAnimation.SetIsWalking(true);
        }
    }
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (playerAnimation != null)
        {
            playerAnimation.SetIsWalking(false);
        }
    }
    private void OnFireStarted(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        StartCoroutine(Fire());
        if (playerAnimation != null)
        {
            playerAnimation.SetIsShooting(true);
        } 
    }
    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        StopAllCoroutines();
        if (playerAnimation != null)
        {
            playerAnimation.SetIsShooting(false);
        }
    }
    private IEnumerator Fire()
    {
        float fireInterval = 60f / roundsPerMinutes;
        while (true)
        {
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
            yield return new WaitForSeconds(fireInterval);
        }
    }
}