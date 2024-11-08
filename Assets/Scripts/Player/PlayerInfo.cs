using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : HPComponent
{
    // 오로지 Enemy의 TracePlayer만을 위한 코드인데, 개선 방안이 분명 존재할것임
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
