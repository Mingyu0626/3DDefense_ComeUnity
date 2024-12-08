using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathReaction : PoolAble
{
    private bool destroyEffectEnd = false;
    private bool destroySoundEnd = false;

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
        if (destroyEffect != null)
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
        AudioSource destroySound = GetComponent<AudioSource>();
        if (destroySound != null && destroySound.clip != null)
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
