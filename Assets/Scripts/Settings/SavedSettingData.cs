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
        private static int resolutionWidth;  // �ػ� �ʺ�
        private static int resolutionHeight; // �ػ� ����
        private static int fullScreenMode;   // ��ü ȭ�� 
        // 0: ��üȭ��, 1: ��ü â���, 3: ������ (2�� ���, Mac ���� â���)

        // Graphic Settings
        private static int textureQuality;   // �ؽ�ó         // 0 ~ 2, 0�� ���� ���ػ� �ؽ�ó ���
        private static int shadowQuality;    // �׸���         // 0 ~ 3, 3�� ���� ���ػ� �ؽ�ó ���
        private static int antiAliasing;     // ��Ƽ �ٸ����   // 0: ������, 2: x2, 4: x4, 8: x8
        private static int vSync;            // ��������ȭ      

        // Gameplay Settings
        private static int mouseSensitivity; // ���콺 ����

        // Sound Settings
        private static int masterVolume;     // ������ ����
        private static int sfxVolume;        // ����Ʈ ����
        private static int bgmVolume;        // BGM ����
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

        private static string GetMemberName<T>(Expression<Func<T>> memberExpression)    // �������� string���� �������ִ� �Լ�. �������� �״�� key�� ���� ����. 
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }


        public static void ApplySetting()
        {
            // ���÷��� ���� ����
            Screen.SetResolution(ResolutionWidth, ResolutionHeight, (FullScreenMode)FullScreenMode);

            // �׷��� ���� ����
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

