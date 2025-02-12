using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBasementView : MonoBehaviour
{
    [SerializeField] private Slider basementHpBar;
    public void SetSliderBasementHP(int hp)
    {
        if (basementHpBar != null)
        {
            basementHpBar.value = hp;
        }
    }
    public void InitSliderBasementHP(int maxValue)
    {
        basementHpBar.maxValue = maxValue;
        basementHpBar.value = maxValue;
    }
}
