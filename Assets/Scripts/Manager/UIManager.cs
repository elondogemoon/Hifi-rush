using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
