using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerView : MonoBehaviour
{
    [SerializeField] private Slider playerHpBar;
    public void SetSliderPlayerHP(int hp)
    {
        if (playerHpBar != null)
        {
            playerHpBar.value = hp;
        }
    }
    public void InitSliderPlayerHP(int maxValue)
    {
        playerHpBar.maxValue = maxValue;
        playerHpBar.value = maxValue;
    }
}
