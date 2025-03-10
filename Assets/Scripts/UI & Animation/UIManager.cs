using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>, ISceneObserver
{
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
        animationManager = new UIAnimationManager();
        animationManager.InitSlidePanelRectTransform
            (ref leftPanelForSlideAnimation, ref rightPanelForSlideAnimation);
        DontDestroyOnLoad(leftPanelForSlideAnimation.root.gameObject);

        pauseAction = InputManager.Instance.Action.UI.Pause;
        pauseAction.performed -= OnPaused;
        pauseAction.performed += OnPaused;
    }
    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        pauseAction.performed -= OnPaused;
    }
    public void OnSceneChanged(string sceneName)
    {
        animationManager.SlideOut(ref leftPanelForSlideAnimation, ref rightPanelForSlideAnimation, 1f, SavedSettingData.ResolutionWidth / 2,
            () => InputManager.Instance.SetAllActionsStateOnSceneName(sceneName),
            () => SetCursorUseableOnSceneName(sceneName));
        InitPopupCanvas();
    }
    public void OnSceneClosed(System.Action callback = null)
    {
        InputManager.Instance.SetAllActionsState(false);
        SetCursorUseable(false);
        animationManager.SlideIn(ref leftPanelForSlideAnimation, ref rightPanelForSlideAnimation, 1f, SavedSettingData.ResolutionWidth / 2, callback);
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
    public void SetCursorUseableOnSceneName(string sceneName)
    {
        if (sceneName.Equals(SceneNames.GameScene))
        {
            SetCursorUseable(false);
        }
        else
        {
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
    public void AddEscapeListener(UnityAction listener)
    {
        RemoveEscapeListener(listener);
        escapeKeyDownEventList.Add(listener);
    }
    public void RemoveEscapeListener(UnityAction listener)
    {
        escapeKeyDownEventList.Remove(listener);
    }

    public WarningPopup CreateWarning2BtnPopup(out WarningPopup popup, string title, string description, 
        string okText, string cancelText, UnityAction okListener, UnityAction cancelListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<WarningPopup>();
        popup.Set2BtnPopup(title, description, okText, cancelText, okListener, cancelListener);
        return popup;
    }
    public WarningPopup CreateWarning2BtnPopup(out WarningPopup popup, string title, string description,
        UnityAction okListener, UnityAction cancelListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<WarningPopup>();
        popup.Set2BtnPopup(title, description, okListener, cancelListener);
        return popup;
    }
    public WarningPopup CreateWarning1BtnPopup(out WarningPopup popup, string title, string description,
        string okText, UnityAction okListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<WarningPopup>();
        popup.Set1BtnPopup(title, description, okText, okListener);
        return popup;
    }
    public WarningPopup CreateWarning1BtnPopup(out WarningPopup popup, string title, string description,
        UnityAction okListener)
    {
        popup = Instantiate(settingsWarningPopupPrefab, popupCanvas).GetComponent<WarningPopup>();
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
