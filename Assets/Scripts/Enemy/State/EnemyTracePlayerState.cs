using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EnemyControlState
{
    public class EnemyTracePlayerState : IEnemyState
    {
        private EnemyController enemyController;
        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            // Debug.Log("EnemyTracePlayerState ¡¯¿‘");
        }

        public void Update()
        {
            TracePlayer();
            if (!enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.goBasementState);
            }

            else if (enemyController.CanAttackPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.attackPlayerState);
            }
        }

        public void Exit()
        {
        }

        private void TracePlayer()
        {
            Vector3 playerPosition = enemyController.GetPlayerPosition();
            Transform enemyTransform = enemyController.transform;

            enemyTransform.rotation = Quaternion.Slerp
                (enemyTransform.rotation,
                Quaternion.LookRotation(playerPosition - enemyTransform.position),
                enemyController.enemyData.RotationSpeed * Time.deltaTime);

            enemyTransform.position = Vector3.MoveTowards
                (enemyTransform.position, playerPosition, 
                enemyController.enemyData.MoveSpeed * Time.deltaTime);
        }
    }
}
