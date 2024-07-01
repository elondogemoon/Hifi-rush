using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [SerializeField] public int _bossHp;
    [SerializeField] public Animator _animator;
    [SerializeField] public NavMeshAgent _navAgent;
    [SerializeField] public Collider _collider;
    private IBossState _bossState;
    public Transform _target;
    private void Start()
    {
        ChangeState(new BossAppearState(this));
        _animator=GetComponent<Animator>();
    }
    public virtual void ChangeState(IBossState newState)
    {
        _bossState?.ExitState();
        _bossState = newState;
        _bossState.EnterState();
    }
    private void Update()
    {
        if (_bossHp <= 50)
        {
            ChangeState(new BossGimicState(this));
        }
        _bossState?.ExecuteOnUpdate();
    }
    public void OnAnimation_ChangeBossState()
    {
        ChangeState(new BossIdleState(this));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            Debug.Log("Ouch");
        }
    }
}
