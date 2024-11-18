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
        StartCoroutine(Attack());
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    bool CheckDistanceFromPlayer()
    {
        return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) < attackableDistance;
    }
    IEnumerator Attack()
    {
        while (true)
        {
            if (Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position) <= attackableDistance)
            {
                Debug.Log("�����Ÿ� ����");
                GameObject enemyBulletGO = ObjectPoolManager.Instance.GetGameObject("EnemyBullet");
                if (enemyBulletGO != null && attackPoint != null)
                {
                    enemyBulletGO.transform.position = attackPoint.transform.position;
                    enemyBulletGO.transform.rotation = attackPoint.transform.rotation;
                }
                yield return new WaitForSeconds(attackInterval);
            }
            yield return null;
        }
    }
}
