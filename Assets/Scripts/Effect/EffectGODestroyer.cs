using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGODestroyer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyAfterEffectEnd());
    }

    IEnumerator DestroyAfterEffectEnd()
    {
        ParticleSystem destroyEffect = GetComponent<ParticleSystem>();
        if (destroyEffect != null)
        {
            destroyEffect.Play();
        }
        yield return new WaitForSeconds(destroyEffect.main.duration);
        Destroy(gameObject);
    }
}
