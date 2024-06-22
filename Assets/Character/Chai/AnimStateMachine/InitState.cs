using UnityEngine;

public class InitState : StateMachineBehaviour
{
    private PlayerMove _playerMove;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerMove == null)
        {
            _playerMove = animator.GetComponentInParent<PlayerMove>();
        }

        if (_playerMove != null)
        {
            _playerMove._CharacterController.enabled = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("OnCloseAttackCombo");
        animator.ResetTrigger("R_Attack");
        animator.ResetTrigger("Parring");
        animator.ResetTrigger("Dash");

        if (_playerMove == null)
        {
            _playerMove = animator.GetComponentInParent<PlayerMove>();
        }

        if (_playerMove != null)
        {
            _playerMove._CharacterController.enabled = true;
        }
    }
}
