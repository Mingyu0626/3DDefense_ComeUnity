using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputAction
{
    public void AddInputActionEvent();

    public void RemoveInputActionEvent();

    public void OnInputActionStarted(InputAction.CallbackContext context);

    public void OnInputActionCanceled(InputAction.CallbackContext context);
}
