using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private UnityEvent sfxAudioSource;
    private UnityEvent bgmAudioSource;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddSfxAudioSource(UnityAction callback)
    {
        sfxAudioSource.RemoveListener(callback);
        sfxAudioSource.AddListener(callback);
    }
    public void AddBgmAudioSource(UnityAction callback)
    {
        bgmAudioSource.RemoveListener(callback);
        bgmAudioSource.AddListener(callback);
    }
    public void RemoveSfxAudioSource(UnityAction callback)
    {
        sfxAudioSource.RemoveListener(callback);
    }
    public void RemoveBgmAudioSource(UnityAction callback)
    {
        bgmAudioSource.AddListener(callback);
    }
    public void InvokeSfxVolumeChangeEvent()
    {
        if (sfxAudioSource != null)
        {
            sfxAudioSource.Invoke();
        }
    }
    public void InvokeBgmVolumeChangeEvent()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.Invoke(); 
        }
    }
}
