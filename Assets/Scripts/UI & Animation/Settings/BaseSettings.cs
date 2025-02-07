using Settings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BaseSettings : MonoBehaviour
{
    private SettingsPanel settingsPanel;
    protected bool isSettingsChanged = false;
    protected virtual void Awake()
    {
        settingsPanel = GetComponentInParent<SettingsPanel>();
    }

    protected virtual void OnEnable()
    {
        settingsPanel = GetComponentInParent<SettingsPanel>();
        if (settingsPanel != null)
        {
            settingsPanel.SetButtonOnClickListener(true, () => OnClickApplyBtn(), () => OnClickCloseBtn());
        }
        else
        {
            Debug.Log("settingsPanel�� null�Դϴ�.");
        }
    }
    protected virtual void OnDisable()
    {

    }
    protected abstract void OnClickApplyBtn();
    protected virtual void OnClickCloseBtn()
    {
        if (CheckCurrentCategorySettingsChange())
        {
            WarningPopup popup = null;
            string title = "[Close Settings]";
            string description = "Changed have not been applied.\n Close Settings?";
            UIManager.Instance.CreateWarning2BtnPopup(out popup, title, description,
                () =>
                {
                    // ����ڰ� ���� ��ư�� ������ ��
                    popup.DestroyPopup();
                    settingsPanel.gameObject.SetActive(false);
                    RestoreChange();
                },
                () =>
                {
                    // ����ڰ� ������ ��ư�� ������ ��
                    popup.DestroyPopup();
                });
        }
        else
        {
            settingsPanel.gameObject.SetActive(false);
        }
    }
    public abstract bool CheckCurrentCategorySettingsChange();
    public abstract void RestoreChange();
    protected void ActivateApplyButton()
    {
        settingsPanel.ActivateApplyButton(CheckCurrentCategorySettingsChange());
    }
    protected void InitOptionItem(Transform itemObj, out TMP_Text valueText, out Button leftBtn, out Button rightBtn, 
        UnityAction OnClickLeftListener, UnityAction OnClickRightListener)
    {
        valueText = itemObj.Find("OptionStateText").GetComponent<TMP_Text>();
        leftBtn = itemObj.Find("ButtonLeft").GetComponent<Button>();
        rightBtn = itemObj.Find("ButtonRight").GetComponent<Button>();

        if (valueText is null || leftBtn is null || rightBtn is null)
        {
            Debug.LogWarning("OptionItem�� ��� �Ҵ���� �ʾҽ��ϴ�.");
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
            Debug.LogWarning("OptionItem�� ��� �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }
        slider.onValueChanged.AddListener(OnValueChangedListener);
    }
}
