using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Prefab")]
    public GameObject settingsWarningPopupPrefab;
    [Header("Heirarchy")]
    public Transform popupCanvas;
    public GameObject pausedPanelGO;
    public GameObject settingsPanelGO;

    private List<UnityAction> escapeKeyDownEventList = new List<UnityAction>();

    InputActions action;
    InputAction pauseAction;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        action = GameManager.Instance.Action;
        pauseAction = action.UI.Pause;
        pauseAction.performed += OnPaused;
        SceneManager.sceneLoaded += OnSceneChanged;
        SetCursorUseable(true);
    }
    private void OnDestroy()
    {
        action.Dispose();
    }
    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        if (scene.name.Equals("GameScene"))
        {
            action.UI.Enable();
            Debug.Log("UI액션 활성화");
        }
        else
        {
            action.UI.Disable();
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
            if (pausedPanelGO != null)
            {
                pausedPanelGO.SetActive(true);
                OpenPausedPanel();
            }
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
