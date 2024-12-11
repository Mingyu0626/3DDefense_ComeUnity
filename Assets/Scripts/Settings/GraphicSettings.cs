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


        private int textureQuality; // �ؽ�ó
        private int shadowQuality; // �׸���
        private int antiAliasing; // ��Ƽ�ٸ����
        private int vSync; // ���� ����ȭ

        void Awake()
        {
            QualitySettings.shadows = UnityEngine.ShadowQuality.All;
        }
    }
}
