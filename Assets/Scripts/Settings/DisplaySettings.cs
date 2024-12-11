using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Settings
{
    public enum DisplayOptionName
    {
        ResolutionWidth,
        ResolutionHeight,
        ScreenMode
    }
    public class DisplaySettings : MonoBehaviour
    {
        List<(int, int)> resolutionList = new List<(int, int)>() 
        { (960, 540), (1280, 720), (1366, 768), (1600, 900), 
            (1920, 1080), (2560, 1440), (3840, 2160) };

        [SerializeField] private Transform resolutionGO;
        [SerializeField] private Transform fullScreenModeGO;

        private (int, int) resolution; // �ػ�
        private int screenMode; // ȭ�� ���(��ü ȭ��, ��ü â ���, â ���)

        void Awake()
        {
            
        }

        void InitGO()
        {
            Screen.SetResolution()
        }

    }
}

