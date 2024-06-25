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
    private IEnemyState _enemyState;

    private void Start()
    {
        ChangeState(new appearState(this));
    }
    public void ChangeState(IEnemyState newState)
    {
        _enemyState?.ExitState();
        _enemyState = newState;
        _enemyState.EnterState();
    }
    public void Update()
    {
        if (_EnemyHp <= 0 && !(_enemyState is DieState))
        {
            ChangeState(new DieState(this));
        }

        _enemyState?.ExecuteOnUpdate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerWeapon")
        {
            ChangeState(new HurtState(this));
            Debug.Log(_EnemyHp);
            _EnemyHp -= 10;
        }
    }
}
