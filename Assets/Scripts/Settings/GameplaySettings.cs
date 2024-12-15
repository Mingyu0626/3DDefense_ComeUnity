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

    // Apply 버튼을 누르기 전 변수
    private int mouseSensitivityOrigin;

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
        mouseSensitivitySlider.value = mouseSensitivity = mouseSensitivityOrigin = SavedSettingData.MouseSensitivity;

        UpdateMouseSensitivity();
    }
    protected override void OnClickApplyBtn()
    {
        // TODO : 옵션값 변화가 감지되었을 때만 Apply버튼 활성화
        // Origin 변수에 변경값을 저장
        mouseSensitivityOrigin = SavedSettingData.MouseSensitivity;
    }
    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();
        if (CheckGameplaySettingsChange())
        {
            mouseSensitivity = SavedSettingData.MouseSensitivity = mouseSensitivityOrigin;
        }
    }
    private void OnMouseSensitivityChanged(float value)
    {
        SavedSettingData.MouseSensitivity = mouseSensitivity = Mathf.RoundToInt(value);
        UpdateMouseSensitivity();
    }
    private void UpdateMouseSensitivity()
    {
        mouseSensitivityText.text = mouseSensitivity.ToString();
    }
    private bool CheckGameplaySettingsChange()
    {
        return SavedSettingData.MouseSensitivity != mouseSensitivityOrigin;
    }
}
