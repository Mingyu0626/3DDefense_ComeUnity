using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSFX : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
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
        audioSourceSfx.clip = audioClip;
        ChangeVolume();

        SavedSettingData.AddListenerSfxVolumeChangeEvent(ChangeVolume);
    }
    private void OnDestroy()
    {
        SavedSettingData.RemoveListenerSfxVolumeChangeEvent(ChangeVolume);
    }
    private void ChangeVolume()
    {
        audioSourceSfx.volume = 
            SavedSettingData.SfxVolume * SavedSettingData.MasterVolume * 0.0001f;
    }
}
