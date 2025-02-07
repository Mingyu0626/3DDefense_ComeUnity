using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : Singleton<InputManager>
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
    public bool IsPlayerActionEnabled() { return action.Player.enabled; }
    public bool IsUIActionEnabled() { return action.UI.enabled; }
    public void SetAllActionsStateOnSceneName(string sceneName)
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
    public void SetAllActionsState(bool isEnable)
    {
        if (isEnable)
        {
            action.Enable();
        }
        else
        {
            action.Disable();
        }
    }
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
