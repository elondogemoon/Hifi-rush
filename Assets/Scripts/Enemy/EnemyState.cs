using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

public class appearState : StateBase
{
    private readonly EnemyView _enemy;
    public appearState(EnemyView enemy)
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
    }
    public override void ExecuteOnUpdate()
    {
        _enemy.ChangeState(new MoveState(_enemy));
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

}

public class MoveState : StateBase
{
    private readonly EnemyView _enemy;

    public MoveState(EnemyView enemy)
    {
        _enemy = enemy;
    }
    public override void EnterState()
    {
        Debug.Log("OnMoveState");

        _enemy._Animator.SetTrigger("Move");
    }
    public override void ExecuteOnUpdate()
    {
        var animInfo = _enemy._Animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime > 1)
        {
            _enemy.ChangeState(new AtkState(_enemy));
        }
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
        Debug.Log(animInfo.normalizedTime);

        
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
