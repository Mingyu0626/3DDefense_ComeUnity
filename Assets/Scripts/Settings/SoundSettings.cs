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


        private int masterVolume;     // ������ ����
        private int sfxVolume;        // ����Ʈ ����
        private int bgmVolume;        // BGM ����

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
                Debug.Log("settingsPanel�� null�Դϴ�.");
            }
        }
        public void OnClickApplyBtn()
        {
            // ���⿡�� SavedSettingData�� �����ϴ� �۾� ����, �̹� ������ ����Ǿ��ִ� ����
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
                Debug.LogWarning("OptionItem�� ��� �Ҵ���� �ʾҽ��ϴ�.");
                return;
            }
            slider.onValueChanged.AddListener(OnValueChangedListener);
        }
    }
}
