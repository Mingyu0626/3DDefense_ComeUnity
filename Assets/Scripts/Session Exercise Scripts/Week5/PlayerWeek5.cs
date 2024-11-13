using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeek5 : HPComponentWeek5
{
    public static PlayerWeek5 Instance { get; private set; } // �ܺο��� ���� ������ �÷��̾� �ν��Ͻ�
    public Transform PlayerTransform { get; private set; } // �÷��̾��� �ǽð� Transform�� �����ϴ� ����

    protected override void Awake()
    {
        base.Awake(); // HPComponent Ŭ������ Awake ȣ��
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Update()
    {
        PlayerTransform = transform; // �÷��̾��� ������ġ ����
    }

    protected override void ApplyDamage(int damage)
    {
        base.ApplyDamage(damage); // HPComponent Ŭ������ ApplyDamage�� ȣ��
        
    }

    protected override void OnTriggerEnter(Collider other) 
    {
        base.OnTriggerEnter(other); // HPComponent Ŭ������ OnTriggerEnter�� ȣ��
    }
}
