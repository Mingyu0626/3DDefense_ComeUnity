using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimation), typeof(PlayerVFX), typeof(PlayerSFX))]
public class PlayerFire : MonoBehaviour, IInputAction
{
    private PlayerAnimation playerAnimation;
    private PlayerVFX playerVfx;
    private PlayerSFX playerSfx;

    private InputAction fireAction;
    private float roundsPerMinutes = 120f;

    [SerializeField] private Transform shootingPoint;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerVfx = GetComponent<PlayerVFX>();
        playerSfx = GetComponent<PlayerSFX>();
        fireAction = InputManager.Instance.Action.Player.Fire;
    }
    public IEnumerator Fire()
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

            playerVfx.PlayFireEffect();
            playerSfx.PlayeFireAudio();
            yield return new WaitForSeconds(fireInterval);
        }
    }
    public void AddInputActionEvent()
    {
        RemoveInputActionEvent();
        fireAction.started += OnInputActionStarted;
        fireAction.canceled += OnInputActionCanceled;
    }

    public void RemoveInputActionEvent()
    {
        fireAction.started -= OnInputActionStarted;
        fireAction.canceled -= OnInputActionCanceled;
    }

    public void OnInputActionStarted(InputAction.CallbackContext context)
    {
        StartFire();
        playerAnimation.SetIsShooting(true);
    }

    public void OnInputActionCanceled(InputAction.CallbackContext context)
    {
        StopFire();
        playerAnimation.SetIsShooting(false);
    }
    public void StartFire()
    {
        StartCoroutine(Fire());
    }
    public void StopFire()
    {
        StopCoroutine(Fire());
        StopAllCoroutines();
    }
}
