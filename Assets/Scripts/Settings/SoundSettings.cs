using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Settings
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private Transform masterVolumeGO;
        [SerializeField] private Transform sfxVolumeGO;
        [SerializeField] private Transform bgmVolumeGO;


        private int masterVolume;     // 마스터 볼륨
        private int sfxVolume;        // 이펙트 볼륨
        private int bgmVolume;        // BGM 볼륨

        private TMP_Text masterVolumeText;
        private TMP_Text sfxVolumeText;
        private TMP_Text bgmVolumeText;

        private Slider masterVolumeSlider;
        private Slider sfxVolumeSlider;
        private Slider bgmVolumeSlider;

        private SettingsPanel settingsPanel;

        private void Awake()
        {
            InitOptionItem(masterVolumeGO, out masterVolumeText, out masterVolumeSlider, 
                OnMasterVolumeChanged);
            InitOptionItem(sfxVolumeGO, out sfxVolumeText, out sfxVolumeSlider,
                OnSfxVolumeChanged);
            InitOptionItem(bgmVolumeGO, out bgmVolumeText, out bgmVolumeSlider,
                OnBgmVolumeChanged);

            settingsPanel = GetComponentInParent<SettingsPanel>();
        }

        private void OnEnable()
        {
            masterVolumeSlider.value = masterVolume = SavedSettingData.MasterVolume;
            sfxVolumeSlider.value = sfxVolume = SavedSettingData.SfxVolume;
            bgmVolumeSlider.value = bgmVolume = SavedSettingData.BgmVolume;
            
            UpdateMasterVolume();
            UpdateSfxVolume();
            UpdateBgmVolume();

            settingsPanel = GetComponentInParent<SettingsPanel>();
            if (settingsPanel is not null)
            {
                settingsPanel.SetApplyOnClickListener(true, OnClickApplyBtn);
                settingsPanel.SetCloseOnClickListener(true, OnClickCloseBtn);
            }
            else
            {
                Debug.Log("settingsPanel이 null입니다.");
            }
        }
        public void OnClickApplyBtn()
        {
            // 여기에서 SavedSettingData에 저장하는 작업 수행, 이미 설정은 적용되어있는 상태
            SavedSettingData.MasterVolume = masterVolume;
            SavedSettingData.SfxVolume = sfxVolume;
            SavedSettingData.BgmVolume = bgmVolume;
        }

        private void OnClickCloseBtn()
        {
            GameManager.Instance.SetSettingsPanelEnable(false);
        }

        private void OnMasterVolumeChanged(float volume)
        {
            masterVolume = Mathf.RoundToInt(volume);
            UpdateMasterVolume();
        }
        private void OnSfxVolumeChanged(float volume)
        {
            sfxVolume = Mathf.RoundToInt(volume);
            UpdateSfxVolume();
        }
        private void OnBgmVolumeChanged(float volume)
        {
            bgmVolume = Mathf.RoundToInt(volume);
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


        private void InitOptionItem(Transform itemObj, out TMP_Text valueText, out Slider slider, UnityAction<float> OnValueChangedListener)
        {
            valueText = itemObj.Find("OptionStateText").GetComponent<TMP_Text>();
            slider = itemObj.Find("Slider").GetComponent<Slider>();

            if (valueText is null || slider is null)
            {
                Debug.LogWarning("OptionItem이 모두 할당되지 않았습니다.");
                return;
            }
            slider.onValueChanged.AddListener(OnValueChangedListener);
        }
    }
}
