using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyControlState
{
    public class EnemyGoBasementState : EnemyState
    {
        private EnemyController enemyController;

        public void Enter(EnemyController controller)
        {
            if (!enemyController)
            {
                enemyController = controller;
            }
            // Debug.Log("EnemyGoBasementState ¡¯¿‘");
        }

        public void Update()
        {
            GoBasement();
            if (enemyController.CanDetectPlayer())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.tracePlayerState);
            }

            else if (enemyController.CanAttackBasement())
            {
                enemyController.enemyStateContext.ChangeState(enemyController.attackBasementState);
            }
        }

        public void Exit()
        {
        }

        private void GoBasement()
        {
            Vector3 basementPosition = enemyController.GetBasementPosition();
            Transform enemyTransform = enemyController.transform;

            enemyTransform.rotation = Quaternion.Slerp
                (enemyTransform.rotation,
                Quaternion.LookRotation(basementPosition - enemyTransform.position),
                enemyController.enemyData.RotationSpeed * Time.deltaTime);

            enemyTransform.position = Vector3.MoveTowards
                (enemyTransform.position, basementPosition, 
                enemyController.enemyData.MoveSpeed * Time.deltaTime);
        }
    }
}

