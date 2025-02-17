using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGAnimation : MonoBehaviour
{
    [SerializeField] private List<Animator> animatorList = new List<Animator>();
    private enum Parameter { isWin }

    private void Awake()
    {
        foreach (var animator in animatorList)
        {
            animator.SetBool(nameof(Parameter.isWin), GameManager.Instance.IsWin);
        }
    }
}
