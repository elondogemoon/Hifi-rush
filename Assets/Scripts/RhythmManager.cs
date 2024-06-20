using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : Singleton<RhythmManager>
{
    public RhythmCircle rhythmCircle;

    void Start()
    {
        rhythmCircle.StartAnimation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rhythmCircle.StartAnimation();
        }
    }
}
