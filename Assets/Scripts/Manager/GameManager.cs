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
    [SerializeField]PlayerAtk _playerAtk;
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] EnemyView _enemyView;
    public void ApplyDamage(int damage, IDamageble character)
    {
        character.ApplyDamage(damage);
    }
    public void FillMP(int amount, IDamageble character)
    {
        character.FillMp(amount);
    }
    public void SuccessParrying()
    {
        _playerAtk._ParryVfx.SetActive(true);
        SfxManager.Instance.OnParringSuccess();
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
}
