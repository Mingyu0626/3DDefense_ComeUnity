using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEditor.SearchService;

public class UIManager : Singleton<UIManager>, ISceneObserver
{
    private FadeManager fadeManager;
    public ref FadeManager FadeManager
    {
        get { return ref fadeManager; }
    }
    [Header("Prefab")]
    public GameObject settingsWarningPopupPrefab;
    public GameObject popupCanvasPrefab;

    private Transform popupCanvas;
    private GameObject pausedPanelGO;
    private GameObject settingsPanelGO;
    private List<UnityAction> escapeKeyDownEventList = new List<UnityAction>();

    private InputActions action;
    private InputAction pauseAction;

    protected override void Awake()
    {
        base.Awake();
        action = GameManager.Instance.Action;
        fadeManager = new FadeManager();
        pauseAction = action.UI.Pause;
        pauseAction.performed -= OnPaused;
        pauseAction.performed += OnPaused;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        pauseAction.performed -= OnPaused;
    }
    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    public void OnSceneChanged(string sceneName)
    {
        InitPopupCanvas();
        if (sceneName.Equals(SceneNames.GameScene))
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
