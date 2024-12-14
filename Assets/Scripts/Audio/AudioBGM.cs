using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBGM : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    private bool isPlayingBgm;

    private float Volume // 
    { 
        get { return SavedSettingData.MasterVolume * SavedSettingData.BgmVolume * 0.0001f; }
        set { }
    }   
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true; 
        audioSource.clip = audioClip;
        audioSource.volume = 0f;
        isPlayingBgm = false;
        SavedSettingData.AddListenerBgmVolumeChangeEvent(ChangeVolume);
    }
    private void OnDestroy()
    {
        SavedSettingData.RemoveListenerBgmVolumeChangeEvent(ChangeVolume);
    }
    private void ChangeVolume()
    {
        audioSource.volume =
            SavedSettingData.BgmVolume * SavedSettingData.MasterVolume * 0.0001f;
    }
    public void Play()
    {
        audioSource.Play();
    }
    public void PlayFade(float fadeTime = 1f)
    {
        StartCoroutine(FadeCoroutine(fadeTime));
    }
    public void Stop()
    {
        audioSource.Stop();
    }

    public void StopFade(float fadeTime = 1f)
    {

    }
    private IEnumerator FadeCoroutine(float fadeTime)
    {
        if (!isPlayingBgm)
        {
            while (audioSource.volume < Volume)
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, Volume, Time.deltaTime / fadeTime);
                yield return null;
            }
        }
        else
        {
            while (0 < audioSource.volume)
            {
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, Time.deltaTime / fadeTime);
                yield return null;
            }
        }
    }
    private IEnumerator StopFadeCoroutine(float fadeTime)
    {
        yield break;
    }
}
