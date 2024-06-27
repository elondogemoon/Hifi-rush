using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public interface IEnemyState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
}

public class StateBase : IEnemyState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExecuteOnUpdate() { }
}

public class AppearState : StateBase
{
    private readonly EnemyView _enemy;

    public AppearState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.SetTrigger("Appear");
    }

    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime > 1)
        {
            _enemy.ChangeState(new IdleState(_enemy));
        }
    }
}

public class IdleState : StateBase
{
    private readonly EnemyView _enemy;

    public IdleState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.ResetTrigger("Appear");
        //_enemy._Animator.SetTrigger("Appear");
    }

    public override void ExecuteOnUpdate()
    {
        if (_enemy.target != null && Vector3.Distance(_enemy.transform.position, _enemy.target.position) < 10f)
        {
            _enemy.ChangeState(new MoveState(_enemy));
        }
    }
}

public class AtkState : StateBase
{
    private readonly EnemyView _enemy;

    public AtkState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.SetTrigger("Attack");
    }

    public override void ExecuteOnUpdate()
    {
        AnimatorStateInfo animatorStateInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        /*var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime > 1)
        {
            _enemy.ChangeState(new IdleState(_enemy));
        }*/
        // 현재 상태 이름을 확인
        //if (animatorStateInfo.IsName("Attack" + "." + "SK_em0200|em0200_atk-gun_002"))
        //{
        //    Debug.Log("서브 스테이트 머신의 애니메이션이 모두 재생되었습니다.");
        //}
    }
    public override void ExitState()
    {
    }
}

public class MoveState : StateBase
{
    private readonly EnemyView _enemy;
    private readonly float attackRange = 5f; // 공격 범위

    public MoveState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.SetTrigger("Move");
        _enemy._navAgent.isStopped = false; // NavMeshAgent 시작
    }

    public override void ExecuteOnUpdate()
    {
        if (_enemy.target != null)
        {
            _enemy._navAgent.SetDestination(_enemy.target.position);

            Vector3 direction = (_enemy.target.position - _enemy.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);

            if (Vector3.Distance(_enemy.transform.position, _enemy.target.position) <= attackRange)
            {
                _enemy.ChangeState(new AtkState(_enemy));
            }
        }

        var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime > 1 && Vector3.Distance(_enemy.transform.position, _enemy.target.position) > attackRange)
        {
            _enemy.ChangeState(new IdleState(_enemy));
        }
    }

    public override void ExitState()
    {
        _enemy._navAgent.isStopped = true; // NavMeshAgent 정지
    }
}

public class DieState : StateBase
{
    private readonly EnemyView _enemy;

    public DieState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.SetTrigger("Die");
    }

    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        _enemy._coiilder.enabled = false;

        if (animInfo.normalizedTime >= 1 && !animInfo.loop)
        {
            _enemy.gameObject.SetActive(false);
        }
    }

    public override void ExitState()
    {
        // 필요에 따라 여기에 상태 종료 시 로직 추가
    }
}

public class HurtState : StateBase
{
    private readonly EnemyView _enemy;

    public HurtState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy._Animator.SetTrigger("Hurt");
    }

    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime >= 1)
        {
            _enemy.ChangeState(new IdleState(_enemy));
        }
    }
}

public class PatternState : StateBase
{
    private readonly EnemyView _enemy;

    public PatternState(EnemyView enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        base.EnterState();
    }
}
