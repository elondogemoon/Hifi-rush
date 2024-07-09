using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChType
{
    Player,
    Enemy,
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] AssistOn _assistOn;
    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerAtk _playerAtk;
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] EnemyView _enemyView;
    [SerializeField] Boss _boss;
    public void ApplyDamage(int damage, IDamageble character)
    {
        character.ApplyDamage(damage);
    }
    public void FillMP(int amount, PlayerStats character)
    {
        character.FillMp(amount);
    }
    public void SuccessParrying()
    {
        _playerAtk._ParryVfx.SetActive(true);
        SfxManager.Instance.OnParringSuccess();
        _playerStats.FillMp(10);
    }
    public void CheckMpAndSkill()
    {
        if (_playerStats != null&&_playerStats._currentMp >= 100)
        {
            _playerStats.ReadyForSkill();
            _playerStats._currentMp = 0;
        }
        else
        {
            Debug.Log("MP부족");
        }
    }
    public void OnBossGimmic()
    {
        _playerAtk.enabled = false;
        _playerMove.enabled = false;
        RhythmManager.Instance.OnBossGimmickRhythm();
        _assistOn.OnBossGimmicAssist();
    }
    public void OffBossGimmic()
    {
        _playerAtk.enabled = true;
        _playerMove.enabled = true;
    }
    public void SuccessGimmic()
    {
        _boss._animator.SetTrigger("Success");
    }
    public void FailGimmic()
    {
        _boss._animator.SetTrigger("Fail");
    }
    public void PlayerDeath()
    {
        _playerMove.enabled = false;
    }
}
