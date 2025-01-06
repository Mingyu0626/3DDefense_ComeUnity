using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeableUI : MonoBehaviour
{
    protected virtual void OnEnable()
    {
        UIManager.Instance.AddEscapeListener(Close);
    }

    protected virtual void OnDisable()
    {
        UIManager.Instance.RemoveEscapeListener(Close);
    }

    protected virtual void Close()
    {

    }
}
