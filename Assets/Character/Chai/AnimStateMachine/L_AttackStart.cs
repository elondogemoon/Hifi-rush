using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_AttackStart : StateMachineBehaviour
{
    private PlayerMove _playerMove; 
    private PlayerAtk _playerAtk;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        _playerMove = animator.GetComponent<PlayerMove>();
        if (_playerMove != null)
        {
           // _playerMove.enabled = false;
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

        
        //animator.SetTrigger("NextAttack");
        //animator.Play("L_Attack2");
    }
}
