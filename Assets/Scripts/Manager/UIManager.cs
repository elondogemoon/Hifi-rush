using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum UIType
{
    SkillReadyPopUp,
    HpBar,
    MpBar,
    RhythmCircle,
    AssistChracter,
}
public class UIManager : Singleton<UIManager>
{
    [SerializeField] Boss _boss;
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] Image _hpBar;
    [SerializeField] Image _MpBar;
    [SerializeField] GameObject _bossHpBar_bg;
    [SerializeField] Image _bossBar;
    private SP_Attack_Gimmic SP_Attack_Gimmic;
    [SerializeField] Image _fight;
   
    public void SuccessCircle()
    {

    }
    public void ApplyDamageToUI()
    {
        if (_hpBar != null)
        {
            _hpBar.fillAmount = (float)_playerStats._currentHp / _playerStats._maxHp;
        }
    }
    public void UpdateMP()
    {
        if (_MpBar != null)
        {
            _MpBar.fillAmount = (float)_playerStats._currentMp / _playerStats._maxMp;
        }
    }
    public void FillMp()
    {
        if (_MpBar != null)
        {
            _MpBar.fillAmount = (float)_playerStats._currentMp / _playerStats._maxMp;

        }
    }
    public void FailReturnMp()
    {
        if(_MpBar != null)
        {
            _MpBar.fillAmount += 80f;
        }
    }
    public void ActiveBossHp()
    {
        _bossHpBar_bg.SetActive(true);
    }

    public void ApplyDamageToUIBoss()
    {
        if (_bossBar != null)
        {
            _bossBar.fillAmount = (float)_boss._currentHp / _boss._bossHp;
        }
    }
    public void StartFight()
    {
        _fight.enabled = true;
        Invoke(nameof(DisableFight), 1f);
    }
    public void DisableFight()
    {
        _fight.enabled = false;
    }
}
