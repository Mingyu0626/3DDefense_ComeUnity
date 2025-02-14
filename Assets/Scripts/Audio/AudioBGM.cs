using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBGM : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSourceBgm;
    private bool isPlayingBgm;

    private float Volume
    { 
        get { return SavedSettingData.MasterVolume * SavedSettingData.BgmVolume * 0.0001f; }
        set { }
    }   
    private void Awake()
    {
        audioSourceBgm = GetComponent<AudioSource>();
        audioSourceBgm.playOnAwake = false;
        audioSourceBgm.loop = true; 
        audioSourceBgm.clip = audioClip;
        audioSourceBgm.volume = 0f;
        isPlayingBgm = false;
        UpdateVolume();
        AudioManager.Instance.AddBgmAudioSource(UpdateVolume);
    }
    private void OnDestroy()
    {
        AudioManager.Instance?.RemoveBgmAudioSource(UpdateVolume);
    }
    private void UpdateVolume()
    {
        audioSourceBgm.volume =
            SavedSettingData.BgmVolume * SavedSettingData.MasterVolume * 0.0001f;
    }
    public void Play()
    {
        audioSourceBgm.Play();
    }
    public void PlayFade(float fadeTime = 1f)
    {
        StartCoroutine(FadeCoroutine(fadeTime));
    }
    public void Stop()
    {
        audioSourceBgm.Stop();
    }

    public void StopFade(float fadeTime = 1f)
    {

    }
    private IEnumerator FadeCoroutine(float fadeTime)
    {
        if (!isPlayingBgm)
        {
            while (audioSourceBgm.volume < Volume)
            {
                audioSourceBgm.volume = Mathf.MoveTowards(audioSourceBgm.volume, Volume, Time.deltaTime / fadeTime);
                yield return null;
            }
        }
        else
        {
            while (0 < audioSourceBgm.volume)
            {
                audioSourceBgm.volume = Mathf.MoveTowards(audioSourceBgm.volume, 0f, Time.deltaTime / fadeTime);
                yield return null;
            }
        }
    }
    private IEnumerator StopFadeCoroutine(float fadeTime)
    {
        yield break;
    }
}
