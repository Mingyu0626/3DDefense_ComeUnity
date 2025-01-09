using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSFX : MonoBehaviour
{
    private AudioSource audioSourceSfx;

    public AudioSource AudioSourceSfx
    {
        get {  return audioSourceSfx; }
        private set { audioSourceSfx = value; }
    }
    private void Awake()
    {
        audioSourceSfx = GetComponent<AudioSource>();
        audioSourceSfx.playOnAwake = false;
        audioSourceSfx.loop = false;
        UpdateVolume();
        AudioManager.Instance.AddSfxAudioSource(UpdateVolume);

    }
    private void OnDestroy()
    {
        AudioManager.Instance.RemoveSfxAudioSource(UpdateVolume);
    }
    private void UpdateVolume()
    {
        audioSourceSfx.volume = 
            SavedSettingData.SfxVolume * SavedSettingData.MasterVolume * 0.0001f;
    }
}
