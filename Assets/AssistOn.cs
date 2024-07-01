using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistOn : MonoBehaviour
{
    [SerializeField] GameObject _assistCharacter;
    [SerializeField] Animator _animator;

    private void OnEnable()
    {
        
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (!_assistCharacter.activeSelf)
            {
                _assistCharacter.SetActive(true);
                _animator.SetTrigger("Appear");
            }
        }
        else
        {
            if(Input.GetKeyUp(KeyCode.LeftAlt))
            {
                _animator.SetTrigger("Disappear");
                Invoke("Disappear", 1);
                
            }
        }
    }
    public void Disappear()
    {
        _assistCharacter.SetActive(false);

    }
    
}
