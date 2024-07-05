using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBossState
{
    void EnterState();
    void ExitState();
    void ExecuteOnUpdate();
}
public class BossState : IBossState
{
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void ExecuteOnUpdate() { }
}

public class BossAppearState : BossState
{
    private readonly Boss _boss;
    public BossAppearState(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Appear");
    }
    public override void ExecuteOnUpdate()
    {
        var animinfo = _boss._animator.GetCurrentAnimatorStateInfo(0);

        if (animinfo.normalizedTime > 0.9)
        {
            _boss.ChangeState(new BossIdleState(_boss));
        }
    }
    public override void ExitState() { }
}

public class BossIdleState : BossState
{
    private readonly Boss _boss;
    private readonly System.Random _random;

    public BossIdleState(Boss boss)
    {
        _boss = boss;
        _random = new System.Random();
    }
    public override void EnterState()
    {
        _boss.StartCoroutine(WaitAndChangeState(0.36f)); 
    }
    public override void ExecuteOnUpdate()
    {
       LookAtTarget();
    }
    private void LookAtTarget()
    {
        if (_boss._target == null) return;

        Vector3 direction = (_boss._target.position - _boss.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        _boss.transform.rotation = Quaternion.Slerp(_boss.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator WaitAndChangeState(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        int attackStateIndex =  _random.Next(4); 
        BossState nextState;

        switch (attackStateIndex)
        {
            case 0:
                nextState = new BossAtk_0_State(_boss);
                break;
            case 1:
                nextState = new BossAtk_1_State(_boss);
                break;
            case 2:
                nextState = new BossAtk_2_State(_boss);
                break;
            case 3:
                nextState = new BossAtk_3_State(_boss);
                break;
            case 4:
                nextState = new BossAtk_4_State(_boss);
                break;
            default:
                nextState = new BossIdleState(_boss); 
                break;
        }

        _boss.ChangeState(nextState);
    }
    public override void ExitState() { }
}

public class BossMoveState : BossState
{
    private readonly Boss _boss;
    public BossMoveState(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState() 
    {
        
    }
}
public class BossAtk_0_State : BossState
{
    private readonly Boss _boss;
    public BossAtk_0_State(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Atk0");
    }
}

public class BossAtk_1_State : BossState
{
    private readonly Boss _boss;
    public BossAtk_1_State(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState() 
    {
        _boss._animator.SetTrigger("Atk1");
    }
    public override void ExecuteOnUpdate()
    {
        
    }
}
public class BossAtk_2_State : BossState
{
    private readonly Boss _boss;
    public BossAtk_2_State(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Atk2");
    }
}
public class BossAtk_3_State : BossState
{
    private readonly Boss _boss;
    public BossAtk_3_State(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Atk3");
    }
}
public class BossAtk_4_State : BossState
{
    private readonly Boss _boss;
    public BossAtk_4_State(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Atk4");
    }
}
public class BossGimicState : BossState
{
    private readonly Boss _boss;
    public BossGimicState(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState()
    {
        _boss._animator.SetTrigger("Gimmic");
        BeatManager.Instance.LowVolume();
        _boss.StartCoroutine(WaitGimmic(5f)) ;
    }

    private IEnumerator WaitGimmic(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.Instance.OnBossGimmic(); 
        
    }
    
}
public class BossDieState : BossState
{
    private readonly Boss _boss;
    public BossDieState(Boss boss)
    {
        _boss = boss;
    }
    public override void EnterState() { }
}



