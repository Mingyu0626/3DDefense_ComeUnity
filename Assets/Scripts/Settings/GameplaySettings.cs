using Settings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameplaySettings : BaseSettings
{
    [SerializeField] private Transform mouseSensitivityGO;

    private int mouseSensitivity;

    private TMP_Text mouseSensitivityText;

    private Slider mouseSensitivitySlider;

    protected override void Awake()
    {
        base.Awake();
        InitOptionItem(mouseSensitivityGO, out mouseSensitivityText, out mouseSensitivitySlider,
            OnMouseSensitivityChanged);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        mouseSensitivitySlider.value = mouseSensitivity = SavedSettingData.MouseSensitivity;

        UpdateMouseSensitivity();
    }
    protected override void OnClickApplyBtn()
    {
        // ���⿡�� SavedSettingData�� �����ϴ� �۾� ����, �̹� ������ ����Ǿ��ִ� ����
        SavedSettingData.MouseSensitivity = mouseSensitivity;
    }
    private void OnClickCloseBtn()
    {
        GameManager.Instance.SetSettingsPanelEnable(false);
    }
    private void OnMouseSensitivityChanged(float value)
    {
        mouseSensitivity = Mathf.RoundToInt(value);
        UpdateMouseSensitivity();
    }
    private void UpdateMouseSensitivity()
    {
        mouseSensitivityText.text = mouseSensitivity.ToString();
    }
}
