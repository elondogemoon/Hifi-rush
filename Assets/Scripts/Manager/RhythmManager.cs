using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : Singleton<RhythmManager>
{
    [SerializeField] AudioSource _AudioSource;
    [SerializeField] AudioClip _SpSuccess;

    [SerializeField] RhythmCircle rhythmCircle;
    [SerializeField] SP_Attack_Gimmic SP_Attack_Gimmic;
    [SerializeField] Boss_Gimmick BossGimmic;
    [SerializeField] PlayerAtk playerAtk;
    [SerializeField] ShaderEffect _effect;
    [SerializeField] Animator _animator;
    public void CallRhythmCircle()
    {
        rhythmCircle.StartAnimation();
    }
    public void OnSPAttackActive()
    {
        Time.timeScale = 0.8f;
        _effect.enabled = true;
        SP_Attack_Gimmic.StartGimic();
    }
    public void OffSpAttack()
    {
        Time.timeScale = 1f;

        _effect.enabled = false;
        SP_Attack_Gimmic.OffAttackGimic();
    }
    public void FailSpAttack()
    {
        Time.timeScale = 1f;

        _effect.enabled = false;
        playerAtk.Animator_Atk.SetTrigger("Fail");
    }
    public void OnSPAttackSuccess()
    {
        _AudioSource.clip = _SpSuccess;
        _AudioSource.Play();
    }
    public void OnBossGimmickRhythm()
    {
        BossGimmic.StartGimic();
    }

    public void FailBossGimmick()
    {
        Debug.Log("fail");
        BossGimmic.OffAttackGimic() ;
        GameManager.Instance.FailGimmic();
    }
    public void SuccessBossGimmick()
    {
      //  BossGimmic.OffAttackGimic();
        Debug.Log("Success");
        
    }
}
