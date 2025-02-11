using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSFX))]
public class PlayerSFX : MonoBehaviour
{
    private AudioSFX audioSfx;
    private void Awake()
    {
        audioSfx = GetComponent<AudioSFX>();
    }
    public void PlayeFireAudio()
    {
        audioSfx.AudioSourceSfx.Play();
        Debug.Log("PlayFireAudio »£√‚");
    }
}
