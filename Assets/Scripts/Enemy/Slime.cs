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
    private bool isCoolDown = false;

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
        if (!isCoolDown)
        {
            StartCoroutine(Attack());
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        CancelInvoke();
    }

    bool CheckDistanceFromPlayer()
    {
        return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) < attackableDistance;
    }
    IEnumerator Attack()
    {
        if (Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) > attackableDistance)
        {
            yield return null;
        }
        else
        {
            GameObject enemyBulletGO = ObjectPoolManager.Instance.GetGameObject("EnemyBullet");
            if (enemyBulletGO != null && attackPoint != null)
            {
                enemyBulletGO.transform.position = attackPoint.transform.position;
                enemyBulletGO.transform.rotation = attackPoint.transform.rotation;
            }
            isCoolDown = true;
            yield return new WaitForSeconds(attackInterval);
            isCoolDown = false;
        }
        yield return null;
    }
}
