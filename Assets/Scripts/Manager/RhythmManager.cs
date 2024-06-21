using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : Singleton<RhythmManager>
{
   [SerializeField] RhythmCircle rhythmCircle;

    

    

    public void CallRhythmCircle()
    {
        rhythmCircle.StartAnimation();
    }
}
