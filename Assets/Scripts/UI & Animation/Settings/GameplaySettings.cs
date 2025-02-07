using Settings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GameplaySettings : BaseSettings
{
    [SerializeField] private Transform mouseSensitivityGO;

    private int mouseSensitivity;

    // Apply ��ư�� ������ �� ����
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
        // TODO : �ɼǰ� ��ȭ�� �����Ǿ��� ���� Apply��ư Ȱ��ȭ
        // Origin ������ ���氪�� ����
        mouseSensitivityOrigin = SavedSettingData.MouseSensitivity;
    }
    protected override void OnClickCloseBtn()
    {
        base.OnClickCloseBtn();
    }
    private void OnMouseSensitivityChanged(float value)
    {
        SavedSettingData.MouseSensitivity = mouseSensitivity = Mathf.RoundToInt(value);
        UpdateMouseSensitivity();
    }
    private void UpdateMouseSensitivity()
    {
        mouseSensitivityText.text = mouseSensitivity.ToString();
        ActivateApplyButton();
    }
    public override bool CheckCurrentCategorySettingsChange()
    {
        return SavedSettingData.MouseSensitivity != mouseSensitivityOrigin;
    }
    public override void RestoreChange()
    {
        mouseSensitivity = SavedSettingData.MouseSensitivity = mouseSensitivityOrigin;
        UpdateMouseSensitivity();
    }
}
