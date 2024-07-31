using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour,IDamageble
{
    [SerializeField] Animator _Animator;
    public int _maxHp = 100;
    public int _currentHp;

    public int _maxMp = 100;
    public int _currentMp;

    private void Awake()
    {
        _currentHp = _maxHp;
        _currentMp = 100;
    }
    private void FixedUpdate()
    {
        UIManager.Instance.UpdateMP();
    }

    public void ReadyForSkill()
    {
        _Animator.SetTrigger("SP_Attack");
        RhythmManager.Instance.OnSPAttackActive();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
            GameManager.Instance.ApplyDamage(10,this);
            _Animator.SetTrigger("Hurt");
            UIManager.Instance.ApplyDamageToUI();
            Debug.Log(_currentHp);
            GameManager.Instance.FillMP(10, this);
            UIManager.Instance.FillMp();
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            _Animator.SetTrigger("Die");
            Invoke(nameof(OnDeath), 2);
        }
    }

    public void FillMp(int amount)
    {
        _currentMp += amount;
        if (_currentMp <= 0 )
        {
            _currentMp = 0;
        }
        
    }
    public void OnDeath()
    {
        
        GameManager.Instance.PlayerDeath();
        SceneManager.LoadScene(2);
        
    }

}
