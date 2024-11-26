using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : Enemy
{
    private int slimeMaxHp = 1;
    private int slimeDamage = 1;
    private float slimeSpeed = 5f;

    private float attackInterval = 3f;
    private float attackableDistance = 20f;

    [SerializeField]
    private GameObject slimeBullet;
    private GameObject attackPoint;

    void Awake()
    {
        maxHp = slimeMaxHp;
        damage = slimeDamage;
        speed = slimeSpeed;
        attackPoint = transform.Find("AttackPoint").gameObject;
    }
    void Start()
    {
        StartCoroutine(Attack());
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) <= attackableDistance)
            {
                Instantiate(slimeBullet, attackPoint.transform.position, attackPoint.transform.rotation);
                yield return new WaitForSeconds(attackInterval);
            }
            yield return null;
        }
    }
}
