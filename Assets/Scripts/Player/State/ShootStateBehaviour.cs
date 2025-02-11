using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStateBehaviour : StateMachineBehaviour
{
    private Transform shootingPoint;
    private Vector3 shootingPointPosition = new Vector3() { x = 0.252f, y = 1.599f, z = 1.681f };    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log($"[{stateInfo.shortNameHash}] 상태 진입");
        shootingPoint = animator.transform.Find("ShootingPoint")?.GetComponent<Transform>();
        if (shootingPoint != null)
        {
            shootingPoint.transform.localPosition = shootingPointPosition;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log($"[{stateInfo.shortNameHash}] 상태 종료");
    }
}
