using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : Singleton<RhythmManager>
{
   [SerializeField] RhythmCircle rhythmCircle;
    [SerializeField] SP_Attack_Gimmic SP_Attack_Gimmic;
    public void CallRhythmCircle()
    {
        rhythmCircle.StartAnimation();
    }
    public void OnSPAttackActive()
    {
        SP_Attack_Gimmic.StartGimic();
    }
    public void OffSpAttack()
    {
        SP_Attack_Gimmic.OffAttackGimic();
    }
}
