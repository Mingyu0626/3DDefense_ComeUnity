using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataModel : MonoBehaviour
{
    public event Action<int> PlayerHPChanged;

    [SerializeField] private int playerHPMax = 100;
    private int playerHP;
    [SerializeField] private float playerSpeed = 20f;

    public int PlayerHPMax { get => playerHPMax; }
    public int PlayerHP
    {
        get => playerHP;
        set
        {
            playerHP = value;
            playerHP = Mathf.Clamp(playerHP, 0, playerHPMax);
            PlayerHPChanged?.Invoke(value);
        }
    }
    public float PlayerSpeed 
    { 
        get => playerSpeed; 
        set 
        { 
            playerSpeed = value;
        } 
    }
}
