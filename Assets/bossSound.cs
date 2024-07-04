using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSound : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip atk1;
    [SerializeField] AudioClip atk2;
    [SerializeField] AudioClip atk3;
    [SerializeField] AudioClip atk4;
    [SerializeField] AudioClip atk5;
    [SerializeField] AudioClip howling;


    public void Atk1()
    {
        _audioSource.clip = atk1;
        _audioSource.Play();
        
    }
    public void Atk2()
    {
        _audioSource.clip = atk2;
        _audioSource.Play();
    }
    public void Atk3()
    {
        _audioSource.clip = atk3;
        _audioSource.Play();
    }
    public void Atk4()
    {
        _audioSource.clip = atk4;
        _audioSource.Play();
    }
    public void Atk5()
    {
        _audioSource.clip = atk5;
        _audioSource.Play();
    }
    public void Howling()
    {
        _audioSource.clip = howling;
        _audioSource.Play();
    }
}
