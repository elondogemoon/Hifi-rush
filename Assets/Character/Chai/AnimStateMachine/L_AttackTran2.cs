using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_AttackTran2 : StateMachineBehaviour
{
    private PlayerMove _playerMove;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        _playerMove = animator.GetComponent<PlayerMove>();
        Debug.Log(_playerMove.enabled);
        if (_playerMove != null)
        {
            //_playerMove.enabled = false;
            _playerMove._CharacterController.enabled = false;
        }
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_playerMove != null)
        {
            //_playerMove.enabled = true;
            _playerMove._CharacterController.enabled = true;
        }

        //animator.SetTrigger("L_AttackTrig3");
        //animator.Play("L_Attack4");
    }

}
