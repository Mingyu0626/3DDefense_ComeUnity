using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Settings
{ 
    public class GraphicSettings : BaseSettings
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

        protected override void Awake()
        {
            base.Awake();
            InitOptionItem(textureGO, out textureQualityText, out textureQualityButtonLeft, out textureQualityButtonRight,
                OnClickTextureQualityLeft, OnClickTextureQualityRight);
            InitOptionItem(shadowGO, out shadowQualityText, out shadowQualityButtonLeft, out shadowQualityButtonRight,
                OnClickShadowQualityLeft, OnClickShadowQualityRight);
            InitOptionItem(antiAliasingGO, out antiAliasingText, out antiAliasingButtonLeft, out antiAliasingButtonRight,
                OnClickAntiAliasingLeft, OnClickAntiAliasingRight);
            InitOptionItem(vSyncGO, out vSyncText, out vSyncButtonLeft, out vSyncButtonRight,
                OnClickVSyncLeft, OnClickVSyncRight);

        }
        protected override void OnEnable()
        {
            base.OnEnable();
            textureQuality = SavedSettingData.TextureQuality;
            shadowQuality = SavedSettingData.ShadowQuality;
            antiAliasing = SavedSettingData.AntiAliasing;
            vSync = SavedSettingData.VSync;

            UpdateTextureQuality();
            UpdateShadowQuality();
            UpdateAntiAliasing();
            UpdateVSync();
        }
        protected override void OnClickApplyBtn()
        {
            // 여기에서 SavedSettingData에 저장하는 작업 수행, 이미 설정은 적용되어있는 상태
            SavedSettingData.TextureQuality = textureQuality;
            SavedSettingData.ShadowQuality = shadowQuality;
            SavedSettingData.AntiAliasing = antiAliasing;
            SavedSettingData.VSync = vSync;
        }
        private void OnClickTextureQualityLeft()
        {
            textureQuality++;
            UpdateTextureQuality();
        }
        private void OnClickTextureQualityRight()
        {
            textureQuality--;
            UpdateTextureQuality();
        }
        private void OnClickShadowQualityLeft()
        {
            shadowQuality--;
            UpdateShadowQuality();
        }
        private void OnClickShadowQualityRight()
        {
            shadowQuality++;
            UpdateShadowQuality();
        }
        private void OnClickAntiAliasingLeft()
        {
            if (antiAliasing == 2) antiAliasing = 0;
            else if (antiAliasing != 0) antiAliasing /= 2;
            UpdateAntiAliasing();
        }
        private void OnClickAntiAliasingRight()
        {
            if (antiAliasing == 0) antiAliasing = 2;
            else if (antiAliasing != 8) antiAliasing *= 2;
            UpdateAntiAliasing();
        }
        private void OnClickVSyncLeft()
        {
            vSync = 0;
            UpdateVSync();
        }
        private void OnClickVSyncRight()
        {
            vSync = 1;
            UpdateVSync();
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
            QualitySettings.globalTextureMipmapLimit = textureQuality;
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
            if (shadowQuality == -1)
            {
                QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;
                QualitySettings.shadowResolution = ShadowResolution.Low;
            }
            else
            {
                QualitySettings.shadows = UnityEngine.ShadowQuality.All;
                QualitySettings.shadowResolution = (ShadowResolution)shadowQuality;
            }
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
            QualitySettings.antiAliasing = antiAliasing;
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
            QualitySettings.vSyncCount = vSync;
        }
    }
}
