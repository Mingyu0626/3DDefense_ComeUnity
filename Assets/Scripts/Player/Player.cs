using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HPComponent
{
    public static Player Instance { get; private set; } // 외부에서 접근 가능한 플레이어 인스턴스
    public Transform PlayerTransform { get; private set; } // 플레이어의 실시간 Transform을 저장하는 변수

    protected override void Awake()
    {
        base.Awake(); // HPComponent 클래스의 Awake 호출
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        PlayerTransform = transform; // 플레이어의 현재위치 저장
    }

    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage); // HPComponent 클래스의 ApplyDamage를 호출
        
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        base.OnTriggerEnter(other); // HPComponent 클래스의 OnTriggerEnter를 호출
    }
}
