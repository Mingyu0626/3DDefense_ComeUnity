using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public enum SoundOptionName
    {
        MainSound
    }

    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private Transform masterVolumeGO;
    }
}
