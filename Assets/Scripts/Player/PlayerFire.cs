using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSFX))]
public class PlayerFire : MonoBehaviour, IInputAction
{
    private InputAction fireAction;
    private PlayerAnimation playerAnimation;
    private float roundsPerMinutes = 120f;

    [SerializeField] private Transform shootingPoint;
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private AudioSFX audioSfx;
    [SerializeField] private AudioSource fireAudio;

    private void Awake()
    {
        fireAction = InputManager.Instance.Action.Player.Fire;
        playerAnimation = GetComponent<PlayerAnimation>();
        audioSfx = GetComponent<AudioSFX>();
        fireAudio = audioSfx.AudioSourceSfx;
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
