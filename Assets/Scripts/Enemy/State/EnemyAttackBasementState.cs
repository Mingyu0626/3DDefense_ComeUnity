using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyAttackBasementState : IEnemyState
    {
        private EnemyController enemyController;
        private float attackBasementInterval = 3f;

        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            // Debug.Log("EnemyAttackPlayerState ¡¯¿‘");
            enemyController.StartStateCoroutine(AttackBasement());
        }

        public void Update()
        {
            if (enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.tracePlayerState);
            }
        }

        public void Exit()
        {
            enemyController.StopStateCoroutine(AttackBasement());
        }

        IEnumerator AttackBasement()
        {
            while (true)
            {

                GameObject enemyBulletGO = ObjectPoolManager.Instance.GetObject("EnemyBullet");
                if (enemyBulletGO != null && enemyController.AttackPoint != null)
                {
                    enemyBulletGO.transform.position = enemyController.AttackPoint.transform.position;
                    enemyBulletGO.transform.rotation = enemyController.AttackPoint.transform.rotation;
                    yield return new WaitForSeconds(attackBasementInterval);
                }
                yield return null;
            }
        }
    }
}
