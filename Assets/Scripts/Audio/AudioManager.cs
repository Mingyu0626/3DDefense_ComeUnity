using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>, ISceneObserver
{
    private UnityEvent sfxAudioSource = new UnityEvent();
    private UnityEvent bgmAudioSource = new UnityEvent();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    private void OnDestroy()
    {
        GameManager.Instance.RemoveObserver(this);
    }
    public void OnSceneChanged(string sceneName)
    {
        switch (sceneName)
        {
            case SceneNames.LobbyScene:
                break;
            case SceneNames.GameScene:
                break;
            case SceneNames.GameEndScene:
                break;
            default:
                break;
        }
    }
    public void AddSfxAudioSource(UnityAction callback)
    {
        sfxAudioSource.RemoveListener(callback);
        sfxAudioSource.AddListener(callback);
    }
    public void AddBgmAudioSource(UnityAction callback)
    {
        bgmAudioSource.RemoveAllListeners();
        bgmAudioSource.AddListener(callback);
    }
    public void RemoveSfxAudioSource(UnityAction callback)
    {
        sfxAudioSource.RemoveListener(callback);
    }
    public void RemoveBgmAudioSource(UnityAction callback)
    {
        bgmAudioSource.RemoveAllListeners();
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
