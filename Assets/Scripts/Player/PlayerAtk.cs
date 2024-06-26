using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    [SerializeField]
    public Animator Animator_Atk;
    [SerializeField]
    GameObject Weapon;
    [SerializeField]
    Collider _ParringColider;

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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Animator_Atk.SetTrigger("Dash");
        }
    }


    public void ActiveSpAttack()
    {
        Animator_Atk.SetTrigger("SP_Attack");
        RhythmManager.Instance.OnSPAttackActive();
    }
    public void ActiveRhythmCircle()
    {
        RhythmManager.Instance.CallRhythmCircle();
    }
    public void OffSpAttack()
    {
        RhythmManager.Instance.OffSpAttack();
    }
    public void OnParrying()
    {
        _ParringColider.enabled = true;
    }
    public void OffParrying()
    {
        _ParringColider.enabled = false;
    }
}
