using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    [Header("Prefab")]
    public GameObject settingsWarningPopupPrefab;
    public GameObject popupCanvasPrefab;

    [Header("Heirarchy")]

    private Transform popupCanvas;
    private GameObject pausedPanelGO;
    private GameObject settingsPanelGO;
    private List<UnityAction> escapeKeyDownEventList = new List<UnityAction>();

    InputActions action;
    InputAction pauseAction;

    protected override void Awake()
    {
        base.Awake();
        action = GameManager.Instance.Action;
        pauseAction = action.UI.Pause;
        pauseAction.performed -= OnPaused;
        pauseAction.performed += OnPaused;
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        pauseAction.performed -= OnPaused;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitPopupCanvas()
    {
        if (popupCanvas == null)
        {
            popupCanvas = Instantiate(popupCanvasPrefab).transform;
            popupCanvas.gameObject.name = popupCanvas.gameObject.name.Replace("(Clone)", "");
            pausedPanelGO = popupCanvas.transform.GetChild(0).gameObject;
            settingsPanelGO = popupCanvas.transform.GetChild(1).gameObject;
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isDestroyed)
        {
            InitPopupCanvas();
            if (scene.name.Equals(SceneNames.GameScene))
            {
                action.UI.Enable();
                SetCursorUseable(false);
            }
            else
            {
                action.UI.Disable();
                SetCursorUseable(true);
            }
        }
    }
    private void OnPaused(InputAction.CallbackContext context)
    {
        if (0 < escapeKeyDownEventList.Count)
        {
            escapeKeyDownEventList[escapeKeyDownEventList.Count - 1].Invoke();
        }
        else
        {
            OpenPausedPanel();
        }
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
    public void AddEscapeListener(UnityAction listener)
    {
        RemoveEscapeListener(listener);
        escapeKeyDownEventList.Add(listener);
    }
    public void RemoveEscapeListener(UnityAction listener)
    {
        escapeKeyDownEventList.Remove(listener);
    }

    public SettingsWarningPopup CreateWarning2BtnPopup(out SettingsWarningPopup popup, string title, string description, 
        string okText, string cancelText, UnityAction okListener, UnityAction cancelListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<SettingsWarningPopup>();
        popup.Set2BtnPopup(title, description, okText, cancelText, okListener, cancelListener);
        return popup;
    }
    public SettingsWarningPopup CreateWarning2BtnPopup(out SettingsWarningPopup popup, string title, string description,
        UnityAction okListener, UnityAction cancelListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<SettingsWarningPopup>();
        popup.Set2BtnPopup(title, description, okListener, cancelListener);
        return popup;
    }
    public SettingsWarningPopup CreateWarning1BtnPopup(out SettingsWarningPopup popup, string title, string description,
        string okText, UnityAction okListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<SettingsWarningPopup>();
        popup.Set1BtnPopup(title, description, okText, okListener);
        return popup;
    }
    public SettingsWarningPopup CreateWarning1BtnPopup(out SettingsWarningPopup popup, string title, string description,
        UnityAction okListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<SettingsWarningPopup>();
        popup.Set1BtnPopup(title, description, okListener);
        return popup;
    }
    public void OpenPausedPanel(bool isOpen = true)
    {
        if (pausedPanelGO != null)
        {
            pausedPanelGO.SetActive(isOpen);
        }
    }
    public void OpenSettingsPanel(bool isOpen = true)
    {
        if (settingsPanelGO != null)
        {
            settingsPanelGO.SetActive(isOpen);
        }
    }
    public bool CheckPausedPanelOpened()
    {
        return pausedPanelGO.activeSelf;
    }
}
