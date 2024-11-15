using System.Collections;
using System.Collections.Generic;
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
        InvokeRepeating(nameof(CheckDistanceFromPlayer), 2f, attackInterval);
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke();
    }

    void CheckDistanceFromPlayer()
    {
        if (Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) < attackableDistance)
        {
            Attack();
        }
    }

    void Attack()
    {
        // ¼¼¼Ç¿ë
        // Instantiate(slimeBullet, transform.position + transform.forward * 2f, transform.rotation);

        GameObject enemyBulletGO = ObjectPoolManager.Instance.GetGameObject("EnemyBullet");
        if (enemyBulletGO != null && attackPoint != null)
        {
            enemyBulletGO.transform.position = attackPoint.transform.position;
            enemyBulletGO.transform.rotation = attackPoint.transform.rotation;
        }
    }

    //IEnumerator Attack()
    //{
    //    yield return null;
    //}
}
