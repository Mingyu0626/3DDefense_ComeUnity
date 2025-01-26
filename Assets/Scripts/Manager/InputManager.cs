using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>, ISceneObserver
{
    private InputActions action;
    public ref InputActions Action
    {
        get { return ref action; }
    }
    protected override void Awake()
    {
        base.Awake();
        action = new InputActions();
    }
    private void Start()
    {
        GameManager.Instance.AddObserver(this);
    }
    public void OnSceneChanged(string sceneName)
    {
        if (sceneName.Equals(SceneNames.GameScene))
        {
            action.Enable();
        }
        else
        {
            action.Disable();
        }
    }
    public bool IsPlayerActionEnabled() { return action.Player.enabled; }
    public bool IsUIActionEnabled() { return action.UI.enabled; }

    public void SetPlayerActionState(bool isEnable)
    {
        if (isEnable)
        {
            action.Player.Enable();
        }
        else
        {
            action.Player.Disable();
        }
    }
    public void SetUIActionState(bool isEnable)
    {
        if (isEnable)
        {
            action.UI.Enable();
        }
        else
        {
            action.UI.Disable();
        }
    }
}
