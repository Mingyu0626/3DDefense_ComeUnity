using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private UnityEvent sfxAudioSource = new UnityEvent();
    private UnityEvent bgmAudioSource = new UnityEvent();

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
