using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    public enum GraphicOptionName
    {
        Texture,
        Shadow,
        AntiAliasing,
        VSync
    }

    public class GraphicSettings : MonoBehaviour
    {
        [SerializeField] private Transform textureGO;
        [SerializeField] private Transform shadowGO;
        [SerializeField] private Transform antiAliasingGO;
        [SerializeField] private Transform vSyncGO;


        private int textureQuality; // 텍스처
        private int shadowQuality; // 그림자
        private int antiAliasing; // 안티앨리어싱
        private int vSync; // 수직 동기화

        void Awake()
        {
            QualitySettings.shadows = UnityEngine.ShadowQuality.All;
        }
    }
}
