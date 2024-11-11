using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basement : HPComponent
{
    // 오로지 Enemy의 TracePlayer만을 위한 코드인데, 개선 방안이 분명 존재할것임
    // Enemy에서 다른 방식으로 공격 대상의 Transform을 실시간으로 변경할 수 있으면
    // 싱글톤으로 할 필요도 없어짐
    public static Basement Instance { get; private set; }
    public Transform BasementTransform { get; private set; }
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
        BasementTransform = transform;
    }

    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        UIManager.Instance.SetBasementHPSlider(curHP);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
