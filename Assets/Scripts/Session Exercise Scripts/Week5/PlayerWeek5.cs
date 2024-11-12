using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeek5 : HPComponentWeek5
{
    public static PlayerWeek5 Instance { get; private set; }
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
        UIManager.Instance.SetPlayerHPSlider(curHP);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
