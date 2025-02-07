using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyController : PoolAble
    {
        public EnemyState tracePlayerState, attackPlayerState, goBasementState, attackBasementState;
        public EnemyStateContext enemyStateContext;
        public EnemyData enemyData;

        [SerializeField]
        private GameObject attackPoint;
        public GameObject AttackPoint
        {  get { return attackPoint; } }


        protected virtual void Awake()
        {

        }
        protected virtual void OnEnable()
        {
            enemyData.CurHP = enemyData.MaxHP;
        }

        private void Start()
        {
            enemyStateContext = new EnemyStateContext(this);
            tracePlayerState = new EnemyTracePlayerState();
            attackPlayerState = new EnemyAttackPlayerState();
            goBasementState = new EnemyGoBasementState();
            attackBasementState = new EnemyAttackBasementState();
            enemyStateContext.ChangeState(goBasementState);
        }

        protected virtual void OnDisable()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerBullet"))
            {
                Bullet playerBullet = other.GetComponent<Bullet>();
                ApplyDamage(playerBullet.GetDamage());
            }
        }

        protected virtual void Update()
        {
            if (enemyStateContext.currentState != null)
            {
                enemyStateContext.currentState.Update();
            }
        }

        private void CreateDeathReactionGO()
        {
            GameObject enemyDeathReactionGO = ObjectPoolManager.Instance.GetGameObject(nameof(EnemyDeathReaction));
            if (enemyDeathReactionGO != null)
            {
                enemyDeathReactionGO.transform.position = transform.position;
            }
        }

        private void ApplyDamage(int damage)
        {
            enemyData.CurHP -= damage;
            if (enemyData.CurHP <= 0)
            {
                ReleaseObject();
                CreateDeathReactionGO();
                StageManager.Instance.OnEnemyKilled();
            }
        }

        public bool CanDetectPlayer()
        { 
            return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position)
                <= enemyData.PlayerDetectableDistance;
        }

        public bool CanAttackPlayer()
        {
            return Vector3.Distance(Player.Instance.PlayerTransform.position, transform.position)
                <= enemyData.PlayerAttackableDistance;
        }

        public bool CanAttackBasement()
        {
            return Vector3.Distance(Basement.Instance.BasementTransform.position, transform.position)
                <= enemyData.BasementAttackableDistance;
        }

        public void StartStateCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine); 
        }

        public void StopStateCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}
