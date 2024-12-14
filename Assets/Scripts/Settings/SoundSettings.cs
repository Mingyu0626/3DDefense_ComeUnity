using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Settings
{
    public class SoundSettings : BaseSettings
    {
        [SerializeField] private Transform masterVolumeGO;
        [SerializeField] private Transform sfxVolumeGO;
        [SerializeField] private Transform bgmVolumeGO;


        private int masterVolume;     // 마스터 볼륨
        private int sfxVolume;        // 이펙트 볼륨
        private int bgmVolume;        // BGM 볼륨

        // Apply 버튼을 누르기 전 볼륨 변수
        private int masterVolumeOrigin;     
        private int sfxVolumeOrigin;       
        private int bgmVolumeOrigin;        

        private TMP_Text masterVolumeText;
        private TMP_Text sfxVolumeText;
        private TMP_Text bgmVolumeText;

        private Slider masterVolumeSlider;
        private Slider sfxVolumeSlider;
        private Slider bgmVolumeSlider;

        protected override void Awake()
        {
            base.Awake();
            InitOptionItem(masterVolumeGO, out masterVolumeText, out masterVolumeSlider, 
                OnMasterVolumeChanged);
            InitOptionItem(sfxVolumeGO, out sfxVolumeText, out sfxVolumeSlider,
                OnSfxVolumeChanged);
            InitOptionItem(bgmVolumeGO, out bgmVolumeText, out bgmVolumeSlider,
                OnBgmVolumeChanged);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            masterVolumeSlider.value = masterVolume = masterVolumeOrigin = SavedSettingData.MasterVolume;
            sfxVolumeSlider.value = sfxVolume = sfxVolumeOrigin = SavedSettingData.SfxVolume;
            bgmVolumeSlider.value = bgmVolume = bgmVolumeOrigin = SavedSettingData.BgmVolume;
            
            UpdateMasterVolume();
            UpdateSfxVolume();
            UpdateBgmVolume();
        }
        protected override void OnClickApplyBtn()
        {
            // Origin 변수에 변경값을 저장
            masterVolumeOrigin = SavedSettingData.MasterVolume;
            sfxVolumeOrigin = SavedSettingData.SfxVolume;
            bgmVolumeOrigin = SavedSettingData.BgmVolume;
        }
        protected override void OnClickCloseBtn()
        {
            base.OnClickCloseBtn();
            if (CheckSoundSettingsChange())
            {
                masterVolume = SavedSettingData.MasterVolume = masterVolumeOrigin;
                sfxVolume = SavedSettingData.SfxVolume = sfxVolumeOrigin;
                bgmVolume = SavedSettingData.BgmVolume = bgmVolumeOrigin;
            }
        }
        private void OnMasterVolumeChanged(float volume)
        {
            SavedSettingData.MasterVolume = masterVolume = Mathf.RoundToInt(volume);
            UpdateMasterVolume();
        }
        private void OnSfxVolumeChanged(float volume)
        {
            SavedSettingData.SfxVolume = sfxVolume = Mathf.RoundToInt(volume);
            UpdateSfxVolume();
        }
        private void OnBgmVolumeChanged(float volume)
        {
            SavedSettingData.BgmVolume = bgmVolume = Mathf.RoundToInt(volume);
            UpdateBgmVolume();
        }
        private void UpdateMasterVolume()
        {
            masterVolumeText.text = masterVolume.ToString();
        }
        private void UpdateSfxVolume()
        {
            sfxVolumeText.text = sfxVolume.ToString();
        }
        private void UpdateBgmVolume()
        {
            bgmVolumeText.text = bgmVolume.ToString();
        }
        private bool CheckSoundSettingsChange()
        {
            return SavedSettingData.MasterVolume != masterVolumeOrigin ||
                SavedSettingData.SfxVolume != sfxVolumeOrigin ||
                SavedSettingData.BgmVolume != bgmVolumeOrigin;
        }
    }
}
