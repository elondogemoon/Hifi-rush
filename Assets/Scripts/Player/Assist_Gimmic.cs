using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assist_Gimmic : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField]AudioSource _audioSource;
    [SerializeField] AudioClip one;
    [SerializeField] AudioClip two;
    [SerializeField] AudioClip three;
    [SerializeField] AudioClip Go;
    [SerializeField] AudioClip _success;
    private void OnEnable()
    {
        Debug.Log("OnEnable");
        _animator.SetTrigger("Gimmic");
        CircleManager.Instance.StartGimmick();
    }
    
    public void OnSuccess()
    {

    }
}
