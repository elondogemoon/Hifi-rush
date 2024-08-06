using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    
    public Animator Animator_Atk;
    [SerializeField]
    GameObject Weapon;
    [SerializeField]
    Collider _ParringColider;
    [SerializeField]
    Collider _WeaponCollider;
    [SerializeField]
    Camera _Camera;
    public GameObject _ParryVfx;
    private void Update()
    {
        Attack();
    }
    
    public void Attack()
    {
        Vector3 cameraDirection = _Camera.transform.forward;
        cameraDirection.y = 0; 
        if (cameraDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.unscaledDeltaTime * 5);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Animator_Atk.SetTrigger("OnCloseAttackCombo");
        }
        if (Input.GetMouseButtonDown(1))
        {
            Animator_Atk.SetTrigger("R_Attack");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.CheckMpAndSkill();
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
        _ParryVfx.SetActive(false);
    }
    public void OnAttack()
    {
        _WeaponCollider.enabled = true;
    }
    public void OffAttack()
    {
        _WeaponCollider.enabled = false;
    }
}
