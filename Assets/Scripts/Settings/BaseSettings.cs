using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseSettings : MonoBehaviour
{
    private SettingsPanel settingsPanel;
    protected virtual void Awake()
    {
        settingsPanel = GetComponentInParent<SettingsPanel>();
    }

    protected virtual void OnEnable()
    {
        settingsPanel = GetComponentInParent<SettingsPanel>();
        if (settingsPanel is not null)
        {
            settingsPanel.SetButtonOnClickListener(true, () => OnClickApplyBtn());
            settingsPanel.SetButtonOnClickListener(true, () => OnClickCloseBtn());
        }
        else
        {
            Debug.Log("settingsPanel이 null입니다.");
        }
    }

    protected virtual void OnClickApplyBtn() 
    {
        Debug.Log("BaseSettings의 OnClickApplyBtn");
    }

    protected virtual void OnClickCloseBtn()
    {
        GameManager.Instance.SetSettingsPanelEnable(false);
    }

    protected void InitOptionItem(Transform itemObj, out TMP_Text valueText, out Button leftBtn, out Button rightBtn, 
        UnityAction OnClickLeftListener, UnityAction OnClickRightListener)
    {
        valueText = itemObj.Find("OptionStateText").GetComponent<TMP_Text>();
        leftBtn = itemObj.Find("ButtonLeft").GetComponent<Button>();
        rightBtn = itemObj.Find("ButtonRight").GetComponent<Button>();

        if (valueText is null || leftBtn is null || rightBtn is null)
        {
            Debug.LogWarning("OptionItem이 모두 할당되지 않았습니다.");
            return;
        }

        leftBtn.onClick.AddListener(OnClickLeftListener);
        rightBtn.onClick.AddListener(OnClickRightListener);
    }

    protected void InitOptionItem(Transform itemObj, out TMP_Text valueText, out Slider slider, 
        UnityAction<float> OnValueChangedListener)
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
