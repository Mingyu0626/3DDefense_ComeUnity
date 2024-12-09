using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyAttackBasementState : EnemyState
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

                GameObject enemyBulletGO = ObjectPoolManager.Instance.GetGameObject("EnemyBullet");
                if (enemyBulletGO != null && enemyController.attackPoint != null)
                {
                    enemyBulletGO.transform.position = enemyController.attackPoint.transform.position;
                    enemyBulletGO.transform.rotation = enemyController.attackPoint.transform.rotation;
                    yield return new WaitForSeconds(attackBasementInterval);
                }
                yield return null;
            }
        }
    }
}
