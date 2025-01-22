using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>, ISceneObserver
{
    private UIFadeManager fadeManager;
    public ref UIFadeManager FadeManager
    {
        get { return ref fadeManager; }
    }
    private UIAnimationManager animationManager;
    public ref UIAnimationManager AnimationManager
    {
        get { return ref animationManager; }
    }

    [Header("Prefab")]
    [SerializeField] private GameObject settingsWarningPopupPrefab;
    [SerializeField] private GameObject popupCanvasPrefab;
    [SerializeField] private RectTransform leftPanelForSlideAnimation;
    [SerializeField] private RectTransform rightPanelForSlideAnimation;

    private Transform popupCanvas;
    private GameObject pausedPanelGO;
    private GameObject settingsPanelGO;
    private List<UnityAction> escapeKeyDownEventList = new List<UnityAction>();


    private InputAction pauseAction;
    protected override void Awake()
    {
        base.Awake();

        fadeManager = new UIFadeManager();

        animationManager = new UIAnimationManager();
        animationManager.InitSlidePanelRectTransform
            (ref leftPanelForSlideAnimation, ref rightPanelForSlideAnimation);
        DontDestroyOnLoad(leftPanelForSlideAnimation.root.gameObject);

        pauseAction = InputManager.Instance.Action.UI.Pause;
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
        animationManager.AnimationSlideOut(1f);
        InitPopupCanvas();
        if (sceneName.Equals(SceneNames.GameScene))
        {
            SetCursorUseable(false);
        }
        else
        {
            SetCursorUseable(true);
        }
    }
    public void OnSceneClosed(System.Action callback)
    {
        animationManager.AnimationSlideIn(1f, callback);
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
    public void SetCursorUseable(bool val)
    {
        if (val)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = val;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = val;
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
