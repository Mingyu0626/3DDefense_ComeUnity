using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsWin { get; private set; } = false;
    private GameObject settingsPanel;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SceneManager.sceneLoaded += OnSceneChanged;
        DontDestroyOnLoad(gameObject);
    }

    public void EndGame(bool isWin)
    {
        IsWin = isWin;
        LoadSceneWithName("GameEndScene");
        SetCursorUseable(true);
    }

    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetCursorUseable(bool useCursor)
    {
        if (useCursor)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        GameObject canvasGO = GameObject.Find("Canvas");
        if (canvasGO is not null)
        {
            Transform settingPanelTransform = canvasGO.transform.Find("SettingsPanel");
            if (settingPanelTransform is not null)
            {
                settingsPanel = settingPanelTransform.gameObject;
            }
        }
    }

    public void SetSettingsPanelEnable(bool val)
    {
        if (settingsPanel is not null)
        {
            settingsPanel.SetActive(val);
        }
    }
}
