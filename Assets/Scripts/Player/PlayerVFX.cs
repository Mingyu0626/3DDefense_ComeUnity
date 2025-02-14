using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireEffect;
    private void Awake()
    {
        
    }
    public void PlayFireEffect()
    {
        if (fireEffect != null)
        {
            fireEffect.Play();
        }
    }
}
