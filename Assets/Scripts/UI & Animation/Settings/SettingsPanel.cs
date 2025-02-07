using Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System.Net;


public class SettingsPanel : EscapeableUI
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

    private int currentActivatedTab = 0; // 0 : 디스플레이, 1 : 그래픽, 2 : 게임플레이, 3 : 사운드
    private int clickedSettingsBtnCategory; // 0 : 디스플레이, 1 : 그래픽, 2 : 게임플레이, 3 : 사운드
    private List<GameObject> tabByCategoryGOList;
    private UnityAction onCloseCallback;

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

        tabByCategoryGOList = new List<GameObject>();
        tabByCategoryGOList.Add(displaySettingsTabGO);
        tabByCategoryGOList.Add(graphicSettingsTabGO);
        tabByCategoryGOList.Add(gameplaySettingTabGO);
        tabByCategoryGOList.Add(soundSettingsTabGO);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SavedSettingData.ApplySetting();
        applyButton.interactable = false;
        OnClickDisplaySettings();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void Close()
    {
        if (onCloseCallback != null)
        {
            onCloseCallback.Invoke();
        }
    }
    public void OnClickAnotherCategoryBtn(int clickedCategoryIndex)
    {
        if (currentActivatedTab == clickedCategoryIndex)
        {
            // 최초 설정 창 오픈시에 필요하지만, 그 이후에는 오버헤드
            ChangeSettingsCategory(clickedCategoryIndex);
            return;
        }

        BaseSettings bs = tabByCategoryGOList[currentActivatedTab].GetComponent<BaseSettings>();
        if (bs != null)
        {
            if (bs.CheckCurrentCategorySettingsChange())
            {
                WarningPopup popup = null;
                string title = "[Close Settings]";
                string description = "Changed have not been applied.\n Change Tab?";
                UIManager.Instance.CreateWarning2BtnPopup(out popup, title, description,
                    () =>
                    {
                        // 사용자가 왼쪽 버튼을 눌렀을 때
                        popup.DestroyPopup();
                        bs.RestoreChange();
                        ChangeSettingsCategory(clickedCategoryIndex);
                        currentActivatedTab = clickedCategoryIndex;
                    },
                    () =>
                    {
                        // 사용자가 오른쪽 버튼을 눌렀을 때
                        popup.DestroyPopup();
                    });
            }
            else
            {
                ChangeSettingsCategory(clickedCategoryIndex);
                currentActivatedTab = clickedCategoryIndex;
            }
        }
    }
    private void OnClickDisplaySettings()
    {
        OnClickAnotherCategoryBtn(0);
    }
    private void OnClickGraphicSettings()
    {
        OnClickAnotherCategoryBtn(1);
    }
    private void OnClickGameplaySettings()
    {
        OnClickAnotherCategoryBtn(2);
    }
    private void OnClickSoundSettings()
    {
        OnClickAnotherCategoryBtn(3);
    }
    private void ChangeSettingsCategory(int categoryIndex)
    {
        for (int i = 0; i < tabByCategoryGOList.Count; i++)
        {
            if (i == categoryIndex)
            {
                tabByCategoryGOList[i].SetActive(true);
            }
            else
            {
                tabByCategoryGOList[i].SetActive(false);
            }
        }

        switch (categoryIndex)
        {
            case 0:
                displaySettingsButtonText.color = Color.blue;
                graphicSettingsButtonText.color = Color.white;
                gameplaySettingsButtonText.color = Color.white;
                soundSettingsButtonText.color = Color.white;
                break;
            case 1:
                displaySettingsButtonText.color = Color.white;
                graphicSettingsButtonText.color = Color.blue;
                gameplaySettingsButtonText.color = Color.white;
                soundSettingsButtonText.color = Color.white;
                break;
            case 2:
                displaySettingsButtonText.color = Color.white;
                graphicSettingsButtonText.color = Color.white;
                gameplaySettingsButtonText.color = Color.blue;
                soundSettingsButtonText.color = Color.white;
                break;
            case 3:
                displaySettingsButtonText.color = Color.white;
                graphicSettingsButtonText.color = Color.white;
                gameplaySettingsButtonText.color = Color.white;
                soundSettingsButtonText.color = Color.blue;
                break;
        }
    }
    public void SetButtonOnClickListener(bool isActive, 
        UnityAction applyListener = null, UnityAction closeListener = null)
    {
        applyButton.gameObject.SetActive(isActive);
        if (isActive && applyListener != null)
        {
            applyButton.onClick.RemoveAllListeners();
            applyButton.onClick.AddListener(applyListener);
        }
        closeButton.gameObject.SetActive(isActive);
        if (isActive && closeListener != null)
        {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(closeListener);
        }

        onCloseCallback = closeListener;
    }
    public void ActivateApplyButton(bool isActive = true)
    {
        applyButton.interactable = isActive;
    }
}
