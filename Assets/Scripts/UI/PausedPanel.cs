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
        base.Close();
        gameObject.SetActive(false);
    }

    private void onClickSettingsButton()
    {
        UIManager.Instance.OpenSettingsPanel();
    }

    private void onClickExitButton()
    {
        GameManager.Instance.LoadSceneWithName(SceneNames.LobbyScene);
        Close();
    }

    private void SetPause(bool isPause)
    {
        if (isPause)
        {
            Time.timeScale = 0f;
            UIManager.Instance.SetCursorUseable(true);
            GameManager.Instance.Action.Player.Disable();
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.SetCursorUseable(false);
            if (!StageManager.Instance.WaitingNextStage)
            {
                GameManager.Instance.Action.Player.Enable();
            }
        }
    }
}
