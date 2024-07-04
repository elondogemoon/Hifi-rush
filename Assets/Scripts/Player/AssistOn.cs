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

        
    }
    public void Disappear()
    {
        _assistCharacter.SetActive(false);

    }
    public void OnGimmic()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //CircleManager.Instance.StartGimmic();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Gimmic"))
        {
            Debug.Log("Gimmic");

        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Debug.Log("alt");
            _assistCharacter.SetActive(true);
            _animator.SetTrigger("Appear");

        }
      //  if (Input.GetKeyUp(KeyCode.LeftAlt))
       // {
       //     _animator.SetTrigger("Disappear");
       //     Invoke("Disappear", 1);

       // }
    }
    private void OnTriggerExit(Collider other)
    {
        _animator.SetTrigger("Disappear");
        Invoke("Disappear", 1);
    }
    public void OnBossGimmicAssist()
    {
        _assistCharacter.SetActive(true);
    }
}
