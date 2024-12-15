using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button displaySettingsButton;
    [SerializeField] private Button graphicSettingsButton;
    [SerializeField] private Button gameplaySettingButton;
    [SerializeField] private Button soundSettingsButton;

    [SerializeField] private GameObject displaySettingsTabGO;
    [SerializeField] private GameObject graphicSettingsTabGO;
    [SerializeField] private GameObject gameplaySettingTabGO;
    [SerializeField] private GameObject soundSettingsTabGO;

    [SerializeField] private Button applyButton;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        displaySettingsButton.onClick.AddListener(OnClickDisplaySettings);
        graphicSettingsButton.onClick.AddListener(OnClickGraphicSettings);
        gameplaySettingButton.onClick.AddListener(OnClickGameplaySettings);
        soundSettingsButton.onClick.AddListener(OnClickSoundSettings);
    }
    private void OnEnable()
    {
        SavedSettingData.ApplySetting();
        OnClickDisplaySettings();
        // 설정 창이 활성화될때, 최초로 보이는 카테고리는 항상 디스플레이 설정이 됨.
    }
    private void OnClickDisplaySettings()
    {
        displaySettingsTabGO.SetActive(true);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(false);
    }
    private void OnClickGraphicSettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(true);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(false);
    }
    private void OnClickGameplaySettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(true);
        soundSettingsTabGO.SetActive(false);
    }
    private void OnClickSoundSettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(true);
    }
    public void SetApplyOnClickListener(bool isActive, UnityAction listener = null)
    {
        applyButton.gameObject.SetActive(isActive);
        if (isActive)
        {
            applyButton.onClick.RemoveAllListeners();
            applyButton.onClick.AddListener(listener);
            // Debug.Log("Apply 버튼에 리스너 장착 완료");
        }
    }
    public void SetCloseOnClickListener(bool isActive, UnityAction listener = null)
    {
        closeButton.gameObject.SetActive(isActive);
        if (isActive)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(listener);
            // Debug.Log("Close 버튼에 리스너 장착 완료");
        }
    }
}
