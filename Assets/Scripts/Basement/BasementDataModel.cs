using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasementDataModel : MonoBehaviour
{
    public event Action<int> BasementHPChanged;

    private readonly int basementHPMax = 100;
    private int basementHP;

    public int BasementHPMax { get => basementHPMax; }
    public int BasementHP
    {
        get => basementHP;
        set
        {
            basementHP = value;
            basementHP = Mathf.Clamp(basementHP, 0, basementHPMax);
            BasementHPChanged?.Invoke(value);
        }
    }
}
