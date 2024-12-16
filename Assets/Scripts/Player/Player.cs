using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HPComponent
{
    // ������ Enemy�� TracePlayer���� ���� �ڵ��ε�, ���� ����� �и� �����Ұ���
    // Enemy���� �ٸ� ������� ���� ����� Transform�� �ǽð����� ������ �� ������
    // �̱������� �� �ʿ䵵 ������
    public static Player Instance { get; private set; }
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

    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage);
        InGameUI.Instance.SetPlayerHPSlider(curHP);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
