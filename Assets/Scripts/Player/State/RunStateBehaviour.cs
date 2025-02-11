using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log($"[{stateInfo.shortNameHash}] 상태 진입");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Debug.Log($"[{stateInfo.shortNameHash}] 상태 종료");
    }
}
