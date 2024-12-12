using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{ 
    public class GraphicSettings : MonoBehaviour
    {
        [SerializeField] private Transform textureGO;
        [SerializeField] private Transform shadowGO;
        [SerializeField] private Transform antiAliasingGO;
        [SerializeField] private Transform vSyncGO;


        private int textureQuality; // 텍스처
        private int shadowQuality;  // 그림자
        private int antiAliasing;   // 안티앨리어싱
        private int vSync;          // 수직 동기화

        private TMP_Text textureQualityText;
        private TMP_Text shadowQualityText;
        private TMP_Text antiAliasingText;
        private TMP_Text vSyncText;

        private Button textureQualityButtonLeft;
        private Button shadowQualityButtonLeft;
        private Button antiAliasingButtonLeft;
        private Button vSyncButtonLeft;

        private Button textureQualityButtonRight;
        private Button shadowQualityButtonRight;
        private Button antiAliasingButtonRight;
        private Button vSyncButtonRight;

        private void Awake()
        {
            
        }
        private void OnEnable()
        {
            textureQuality = SavedSettingData.TextureQuality;
            shadowQuality = SavedSettingData.ShadowQuality;
            antiAliasing = SavedSettingData.AntiAliasing;
            vSync = SavedSettingData.VSync;
        }

        private void OnClickTextureQualityLeft()
        {
            textureQuality++;
        }
        private void OnClickTextureQualityRight()
        {
            textureQuality--;
        }
        private void OnClickShadowQualityLeft()
        {
            shadowQuality--;
        }
        private void OnClickShadowQualityRight()
        {
            shadowQuality++;
        }
        private void OnClickAntiAliasingLeft()
        {
            if (antiAliasing == 2) antiAliasing = 0;
            else if (antiAliasing != 0) antiAliasing /= 2;
        }
        private void OnClickAntiAliasingRight()
        {
            if (antiAliasing == 0) antiAliasing = 2;
            else if (antiAliasing != 8) antiAliasing *= 2;
        }
        private void OnClickVSyncLeft()
        {
            vSync = 0;
        }
        private void OnClickVSyncRight()
        {
            vSync = 1;
        }

        private void UpdateTextureQuality()
        {
            switch (textureQuality)
            {
                case 0:
                    textureQualityText.text = "High";
                    break;
                case 1:
                    textureQualityText.text = "Medium";
                    break;
                case 2:
                    textureQualityText.text = "Low";
                    break;
                default:
                    textureQualityText.text = "Error";
                    break;
            }
            textureQualityButtonLeft.interactable = textureQuality != 2;
            textureQualityButtonRight.interactable = textureQuality != 0;
        }

        private void UpdateShadowQuality()
        {
            switch (shadowQuality)
            {
                case -1:
                    shadowQualityText.text = "Off";
                    break;
                case 0:
                    shadowQualityText.text = "Low";
                    break;
                case 1:
                    shadowQualityText.text = "Medium";
                    break;
                case 2:
                    shadowQualityText.text = "High";
                    break;
                case 3:
                    shadowQualityText.text = "Very High";
                    break;
                default:
                    shadowQualityText.text = "Error";
                    break;
            }
            shadowQualityButtonLeft.interactable = shadowQuality != -1;
            shadowQualityButtonRight.interactable = shadowQuality != 3;
        }

        private void UpdateAntiAliasing()
        {
            switch (antiAliasing)
            {
                case 0:
                    antiAliasingText.text = "Off";
                    break;
                case 2:
                    antiAliasingText.text = "MSAA x2";
                    break;
                case 4:
                    antiAliasingText.text = "MSAA x4";
                    break;
                case 8:
                    antiAliasingText.text = "MSAA x8";
                    break;
                default:
                    antiAliasingText.text = "Error";
                    break;
            }
            antiAliasingButtonLeft.interactable = antiAliasing != 0;
            antiAliasingButtonRight.interactable = antiAliasing != 8;
        }

        private void UpdateVSync()
        {
            switch (vSync)
            {
                case 0:
                    vSyncText.text = "Off";
                    break;
                case 1:
                    vSyncText.text = "On";
                    break;
                default:
                    vSyncText.text = "Error";
                    break;
            }
            vSyncButtonLeft.interactable = vSync != 0;
            vSyncButtonRight.interactable = vSync != 1;
        }
    }
}
