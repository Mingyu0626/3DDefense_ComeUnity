using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerFire))]
public class PlayerAction : MonoBehaviour
{
    private List<IInputAction> interfaceInputActionList;
    private void Awake()
    {
        interfaceInputActionList = new List<IInputAction>
        { GetComponent<PlayerMovement>(), GetComponent<PlayerFire>() };
        AddActionEvent();
    }
    private void OnDestroy()
    {
        RemoveActionEvent();
    }
    public void AddActionEvent()
    {
        for (int i = 0; i < interfaceInputActionList.Count; i++)
        {
            interfaceInputActionList[i].AddInputActionEvent();
        }
    }
    public void RemoveActionEvent()
    {
        for (int i = 0; i < interfaceInputActionList.Count; i++)
        {
            interfaceInputActionList[i].RemoveInputActionEvent();
        }
    }
}
