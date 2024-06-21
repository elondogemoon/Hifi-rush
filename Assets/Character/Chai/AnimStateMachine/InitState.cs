using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitState : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("OnCloseAttackCombo");
        animator.ResetTrigger("R_Attack");
        animator.ResetTrigger("Parring");
        animator.ResetTrigger("Dash");
    }

}
