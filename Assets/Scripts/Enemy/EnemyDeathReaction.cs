using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSFX))]
public class EnemyDeathReaction : PoolAble
{
    private AudioSFX audioSfx;
    private bool destroyEffectEnd = false;
    private bool destroySoundEnd = false;

    private void Awake()
    {
        audioSfx = GetComponent<AudioSFX>();
    }

    private void OnEnable()
    {
        StartCoroutine(PlayDestroySound());
        StartCoroutine(PlayDestroyEffect());
    }

    bool checkDestroyCondition()
    {
        return destroyEffectEnd && destroySoundEnd;
    }

    IEnumerator PlayDestroyEffect()
    {
        ParticleSystem destroyEffect = GetComponent<ParticleSystem>();
        if (destroyEffect is not null)
        {
            destroyEffect.Play();
            yield return new WaitForSeconds(destroyEffect.main.duration);
            destroyEffectEnd = true;

            if (checkDestroyCondition())
            {
                ReleaseObject();
            }
        }
 
    }

    IEnumerator PlayDestroySound()
    {
        AudioSource destroySound = audioSfx.AudioSourceSfx;
        if (destroySound is not null && destroySound.clip is not null)
        {
            destroySound.Play();
            yield return new WaitForSeconds(destroySound.clip.length);
            destroySoundEnd = true;

            if (checkDestroyCondition())
            {
                ReleaseObject();
            }
        }
    }
}
