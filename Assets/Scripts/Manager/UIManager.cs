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
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] Image _hpBar;
    [SerializeField] Image _MpBar;
    private HashSet<UIType> _openUiDic = new HashSet<UIType>();
    private SP_Attack_Gimmic SP_Attack_Gimmic;
    private void OpenUI(UIType uiType, GameObject UIobj)
    {
        if (_openUiDic.Contains(uiType) == false)
        {
        UIobj.SetActive(true);
        _openUiDic.Add(uiType);
        }
    }
    private void CloseUI(UIType uiType, GameObject UIobj)
    {
        if (_openUiDic.Contains(uiType))
        {
           UIobj.SetActive(false);
            _openUiDic.Remove(uiType);
        }
    }
    private void CircleOn(UIType uiType, GameObject UIobj)
    {

    }
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
}
