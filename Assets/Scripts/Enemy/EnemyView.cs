using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    [SerializeField] public int _EnemyHp;
    [SerializeField] public Animator _Animator;
    [SerializeField] public Collider _WeaponCollider;
    [SerializeField] public NavMeshAgent _navAgent;
    [SerializeField] public Collider _coiilder;
    [SerializeField] private AudioSource _audioSource;
    private IEnemyState _enemyState;
    public Transform target;

    private void Start()
    {
        ChangeState(new AppearState(this));
        _Animator = GetComponent<Animator>();
    }

    public virtual void ChangeState(IEnemyState newState)
    {
        _enemyState?.ExitState();
        _enemyState = newState;
        _enemyState.EnterState();
        
    }

    private void Update()
    {
        if (_EnemyHp <= 0 && !(_enemyState is DieState))
        {
            ChangeState(new DieState(this));
        }
       
        _enemyState?.ExecuteOnUpdate();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            ChangeState(new HurtState(this));
            _EnemyHp -= 10;
            
            
        }
        if(other.tag == "Parrying")
        {
            OnParryed();
            GameManager.Instance.SuccessParrying();
            _WeaponCollider.enabled = false;
        }
    }
    public void OnParryed()
    {
        //ChangeState(new HurtState(this));
    }
    public virtual void OnAttackState()
    {
        _WeaponCollider.enabled = true;
    }
    public virtual void OnAttackEnd()
    {
        _WeaponCollider.enabled = false;
    }
    public void AtkSound()
    {
        _audioSource.Play();
    }
}
