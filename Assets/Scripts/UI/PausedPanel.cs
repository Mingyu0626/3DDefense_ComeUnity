using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PausedPanel : EscapeableUI
{
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        settingsButton.onClick.AddListener(onClickSettingsButton);
        exitButton.onClick.AddListener(onClickExitButton);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        SetPause(true);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        SetPause(false);
    }
    protected override void Close()
    {
        SetPause(false);
    }

    private void onClickSettingsButton()
    {
        UIManager.Instance.OpenSettingsPanel();
    }

    private void onClickExitButton()
    {
        GameManager.Instance.LoadSceneWithName("LobbyScene");
    }

    private void SetPause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0f;
            GameManager.Instance.Action.Player.Disable();
            UIManager.Instance.SetCursorUseable(true);
        }
        else
        {
            Time.timeScale = 1f;
            GameManager.Instance.Action.Player.Enable();
            UIManager.Instance.SetCursorUseable(false);
        }
    }
}