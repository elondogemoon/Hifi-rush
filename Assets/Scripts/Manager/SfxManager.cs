using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : Singleton<SfxManager>
{
    [SerializeField] AudioClip _CircleSuccess;
    [SerializeField] AudioClip _SpSuccess;
    [SerializeField] AudioSource _AudioSource;
    public void OnCircleSuccess()
    {
        _AudioSource.clip = _CircleSuccess;
        _AudioSource.Play();
    }
    public void OnSPAttackSuccess()
    {
        _AudioSource.clip = _SpSuccess;
        _AudioSource.Play();
    }
}
