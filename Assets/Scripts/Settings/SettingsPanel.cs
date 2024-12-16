using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Net;


public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button displaySettingsButton;
    [SerializeField] private Button graphicSettingsButton;
    [SerializeField] private Button gameplaySettingButton;
    [SerializeField] private Button soundSettingsButton;

    private TextMeshProUGUI displaySettingsButtonText;
    private TextMeshProUGUI graphicSettingsButtonText;
    private TextMeshProUGUI gameplaySettingsButtonText;
    private TextMeshProUGUI soundSettingsButtonText;

    [SerializeField] private GameObject displaySettingsTabGO;
    [SerializeField] private GameObject graphicSettingsTabGO;
    [SerializeField] private GameObject gameplaySettingTabGO;
    [SerializeField] private GameObject soundSettingsTabGO;

    [SerializeField] private Button applyButton;
    [SerializeField] private Button closeButton;

    // private int currentActivatedTab = 0; // 0 : 디스플레이, 1 : 그래픽, 2 : 게임플레이, 3 : 사운드
    // private int previousActivatedTab = 0; // 0 : 디스플레이, 1 : 그래픽, 2 : 게임플레이, 3 : 사운드

    private void Awake()
    {
        displaySettingsButton.onClick.AddListener(OnClickDisplaySettings);
        graphicSettingsButton.onClick.AddListener(OnClickGraphicSettings);
        gameplaySettingButton.onClick.AddListener(OnClickGameplaySettings);
        soundSettingsButton.onClick.AddListener(OnClickSoundSettings);

        displaySettingsButtonText = displaySettingsButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        graphicSettingsButtonText = graphicSettingsButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        gameplaySettingsButtonText = gameplaySettingButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        soundSettingsButtonText = soundSettingsButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        SavedSettingData.ApplySetting();
        OnClickDisplaySettings();
        
    }
    private void OnClickDisplaySettings()
    {
        displaySettingsTabGO.SetActive(true);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(false);

        displaySettingsButtonText.color = Color.yellow;
        graphicSettingsButtonText.color = Color.white;
        gameplaySettingsButtonText.color = Color.white;
        soundSettingsButtonText.color = Color.white;
    }
    private void OnClickGraphicSettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(true);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(false);

        displaySettingsButtonText.color = Color.white;
        graphicSettingsButtonText.color = Color.yellow;
        gameplaySettingsButtonText.color = Color.white;
        soundSettingsButtonText.color = Color.white;
    }
    private void OnClickGameplaySettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(true);
        soundSettingsTabGO.SetActive(false);

        displaySettingsButtonText.color = Color.white;
        graphicSettingsButtonText.color = Color.white;
        gameplaySettingsButtonText.color = Color.yellow;
        soundSettingsButtonText.color = Color.white;
    }
    private void OnClickSoundSettings()
    {
        displaySettingsTabGO.SetActive(false);
        graphicSettingsTabGO.SetActive(false);
        gameplaySettingTabGO.SetActive(false);
        soundSettingsTabGO.SetActive(true);

        displaySettingsButtonText.color = Color.white;
        graphicSettingsButtonText.color = Color.white;
        gameplaySettingsButtonText.color = Color.white;
        soundSettingsButtonText.color = Color.yellow;
    }
    public void SetButtonOnClickListener(bool isActive, UnityAction listener = null)
    {
        applyButton.gameObject.SetActive(isActive);
        if (isActive && listener != null)
        {
            applyButton.onClick.RemoveAllListeners();
            applyButton.onClick.AddListener(listener);
        }
    }
}
