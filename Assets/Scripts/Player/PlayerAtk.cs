using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField]
    Animator Animator_Atk;
    [SerializeField]
    GameObject Weapon;


    private void Update()
    {
        Attack();
        
    }

    public void Attack()
    {
        if (Input.GetMouseButton(0))
        {
            Animator_Atk.SetTrigger("OnCloseAttackCombo");
        }
        if (Input.GetMouseButton(1))
        {
            Animator_Atk.SetTrigger("R_Attack");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            ActiveSpAttack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Animator_Atk.SetTrigger("Parring");
        }
    }


    public void ActiveSpAttack()
    {
        Animator_Atk.SetTrigger("SP_Attack");
    }

    
}
