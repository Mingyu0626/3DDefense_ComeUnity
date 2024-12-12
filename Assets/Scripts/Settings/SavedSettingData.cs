using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;

namespace Settings
{
    public static class SavedSettingData
    {
        class Event : UnityEvent { }

        // Display Settings
        private static int resolutionWidth;  // 해상도 너비
        private static int resolutionHeight; // 해상도 높이
        private static int fullScreenMode;   // 전체 화면 
        // 0: 전체화면, 1: 전체 창모드, 3: 윈도우 (2의 경우, Mac 전용 창모드)

        // Graphic Settings
        private static int textureQuality;   // 텍스처         // 0 ~ 2, 0이 제일 고해상도 텍스처 사용
        private static int shadowQuality;    // 그림자         // 0 ~ 3, 3이 제일 고해상도 텍스처 사용
        private static int antiAliasing;     // 안티 앨리어싱   // 0: 미적용, 2: x2, 4: x4, 8: x8
        private static int vSync;            // 수직동기화      

        // Gameplay Settings
        private static int mouseSensitivity; // 마우스 감도

        // Sound Settings
        private static int masterVolume;     // 마스터 볼륨
        private static int sfxVolume;        // 이펙트 볼륨
        private static int bgmVolume;        // BGM 볼륨
        static Event BgmVolumeChangeEvent = new Event();
        public static void AddListenerBgmVolumeChangeEvent(UnityAction callback) { BgmVolumeChangeEvent.RemoveListener(callback); BgmVolumeChangeEvent.AddListener(callback); }
        public static void RemoveListenerBgmVolumeChangeEvent(UnityAction callback) { BgmVolumeChangeEvent.RemoveListener(callback); }
        public static void InvokeBgmVolumeChangeEvent() { BgmVolumeChangeEvent.Invoke(); }
        static Event SfxVolumeChangeEvent = new Event();
        public static void AddListenerSfxVolumeChangeEvent(UnityAction callback) { SfxVolumeChangeEvent.RemoveListener(callback); SfxVolumeChangeEvent.AddListener(callback); }
        public static void RemoveListenerSfxVolumeChangeEvent(UnityAction callback) { SfxVolumeChangeEvent.RemoveListener(callback); }
        public static void InvokeSfxVolumeChangeEvent() { SfxVolumeChangeEvent.Invoke(); }


        

        public static int ResolutionWidth
        {
            get { return resolutionWidth; }
            set 
            { 
                resolutionWidth = value;
                PlayerPrefs.SetInt(GetMemberName(() => resolutionWidth), value);
            }
        }
        public static int ResolutionHeight
        {
            get { return resolutionHeight; }
            set 
            { 
                resolutionHeight = value;
                PlayerPrefs.SetInt(GetMemberName(() => resolutionHeight), value);
            }
        }
        public static int FullScreenMode
        {
            get { return fullScreenMode; }
            set 
            {
                fullScreenMode = value;
                PlayerPrefs.SetInt(GetMemberName(() => fullScreenMode), value);
            }
        }
        public static int TextureQuality
        {
            get { return textureQuality; }
            set 
            { 
                textureQuality = value; 
                PlayerPrefs.SetInt(GetMemberName(() => textureQuality), value); 
            }
        }
        public static int ShadowQuality
        {
            get { return shadowQuality; }
            set 
            { 
                shadowQuality = value; 
                PlayerPrefs.SetInt(GetMemberName(() => shadowQuality), value); 
            }
        }
        public static int AntiAliasing
        {
            get { return antiAliasing; }
            set 
            { 
                antiAliasing = value; 
                PlayerPrefs.SetInt(GetMemberName(() => antiAliasing), value); 
            }
        }
        public static int VSync
        {
            get { return vSync; }
            set 
            { 
                vSync = value; 
                PlayerPrefs.SetInt(GetMemberName(() => vSync), value); 
            }
        }

        public static int MouseSensitivity
        {
            get { return mouseSensitivity; }
            set
            {
                mouseSensitivity = value;
                PlayerPrefs.SetInt(GetMemberName(() => mouseSensitivity), value);
            }
        }

        public static int MasterVolume
        {
            get { return masterVolume; }
            set
            {
                masterVolume = value; 
                PlayerPrefs.SetInt(GetMemberName(() => masterVolume), value);
                InvokeBgmVolumeChangeEvent(); InvokeSfxVolumeChangeEvent();
            }
        }
        public static int BgmVolume
        {
            get { return bgmVolume; }
            set
            {
                bgmVolume = value; 
                PlayerPrefs.SetInt(GetMemberName(() => bgmVolume), value);
                InvokeBgmVolumeChangeEvent();
            }
        }
        public static int SfxVolume
        {
            get { return sfxVolume; }
            set
            {
                sfxVolume = value; 
                PlayerPrefs.SetInt(GetMemberName(() => sfxVolume), value);
                InvokeSfxVolumeChangeEvent();
            }
        }

        static SavedSettingData()
        {
            resolutionWidth = PlayerPrefs.GetInt(GetMemberName(() => resolutionWidth), 1920);
            resolutionHeight = PlayerPrefs.GetInt(GetMemberName(() => resolutionHeight), 1080);
            fullScreenMode = PlayerPrefs.GetInt(GetMemberName(() => fullScreenMode), 0);

            textureQuality = PlayerPrefs.GetInt(GetMemberName(() => textureQuality), 0);
            shadowQuality = PlayerPrefs.GetInt(GetMemberName(() => shadowQuality), 2);
            antiAliasing = PlayerPrefs.GetInt(GetMemberName(() => antiAliasing), 0);
            vSync = PlayerPrefs.GetInt(GetMemberName(() => vSync), 0);

            mouseSensitivity = PlayerPrefs.GetInt(GetMemberName(() => mouseSensitivity), 20);

            masterVolume = PlayerPrefs.GetInt(GetMemberName(() => masterVolume), 100);
            sfxVolume = PlayerPrefs.GetInt(GetMemberName(() => sfxVolume), 100);
            bgmVolume = PlayerPrefs.GetInt(GetMemberName(() => bgmVolume), 100);
        }

        private static string GetMemberName<T>(Expression<Func<T>> memberExpression)    // 변수명을 string으로 리턴해주는 함수. 변수명을 그대로 key로 쓰기 위함. 
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }


        public static void ApplySetting()
        {
            // 디스플레이 설정 적용
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, (FullScreenMode)FullScreenMode);

            // 그래픽 설정 적용
            QualitySettings.globalTextureMipmapLimit = TextureQuality;
            if (ShadowQuality == -1)
            {
                QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;
                QualitySettings.shadowResolution = ShadowResolution.Low;
            }
            else
            {
                QualitySettings.shadows = UnityEngine.ShadowQuality.All;
                QualitySettings.shadowResolution = (ShadowResolution)ShadowQuality;
            }
            QualitySettings.antiAliasing = AntiAliasing;
            QualitySettings.vSyncCount = VSync;
        }
    }
}

