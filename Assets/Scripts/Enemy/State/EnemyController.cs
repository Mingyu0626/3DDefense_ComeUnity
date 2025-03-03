using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyController : PoolAble
    {
        public IEnemyState tracePlayerState, attackPlayerState, goBasementState, attackBasementState;
        public EnemyStateContext enemyStateContext;
        public EnemyData enemyData;

        [SerializeField] private GameObject attackPoint;
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
                if (playerBullet != null)
                {
                    ApplyDamage(playerBullet.GetDamage());
                    if (CheckDeath())
                    {
                        HandleDeath();
                    }
                }
            }
        }
        protected virtual void Update()
        {
            if (enemyStateContext.currentState != null)
            {
                enemyStateContext.currentState.Update();
            }
        }
        public void StartStateCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }

        public void StopStateCoroutine(IEnumerator coroutine)
        {
            StopCoroutine(coroutine);
        }
        private void CreateDeathReactionGO()
        {
            GameObject enemyDeathReactionGO = ObjectPoolManager.Instance.GetObject(nameof(EnemyDeathReaction));
            if (enemyDeathReactionGO != null)
            {
                enemyDeathReactionGO.transform.position = transform.position;
            }
        }

        private void ApplyDamage(int damage)
        {
            enemyData.CurHP -= damage;
        }
        private bool CheckDeath()
        {
            return enemyData.CurHP <= 0;
        }
        private void HandleDeath()
        {
            ReleaseObject();
            CreateDeathReactionGO();
            StageManager.Instance.OnEnemyKilled();
        }

        public bool CanDetectPlayer()
        { 
            return Vector3.Distance(GetPlayerPosition(), transform.position)
                <= enemyData.PlayerDetectableDistance;
        }

        public bool CanAttackPlayer()
        {
            return Vector3.Distance(GetPlayerPosition(), transform.position)
                <= enemyData.PlayerAttackableDistance;
        }

        public bool CanAttackBasement()
        {
            return Vector3.Distance(GetBasementPosition(), transform.position)
                <= enemyData.BasementAttackableDistance;
        }
        public Vector3 GetPlayerPosition() { return Player.Instance.transform.position; }
        public Vector3 GetBasementPosition() { return Basement.Instance.transform.position; }
    }
}
