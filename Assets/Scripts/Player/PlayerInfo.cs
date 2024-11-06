using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : HPComponent
{
    public static PlayerInfo Instance { get; private set; }
    public Transform PlayerTransform { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        PlayerTransform = transform;
    }
}
