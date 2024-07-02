using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour,IDamageble
{
    public int _currentHp;
    [SerializeField] public int _bossHp=200;
    [SerializeField] public Animator _animator;
    [SerializeField] public NavMeshAgent _navAgent;
    [SerializeField] public Collider _collider;
    [SerializeField] public Collider _atkCollider;
    [SerializeField] public AudioSource _audioSource;

    private IBossState _bossState;
    public Transform _target;
    private void Start()
    {
        _currentHp = _bossHp;
        ChangeState(new BossAppearState(this));
        _animator=GetComponent<Animator>();
    }
    public virtual void ChangeState(IBossState newState)
    {
        _bossState?.ExitState();
        _bossState = newState;
        _bossState.EnterState();
    }
    private void Update()
    {
        if (_currentHp <= 50)
        {
            ChangeState(new BossGimicState(this));
        }
        _bossState?.ExecuteOnUpdate();
    }
    public void OnAnimation_ChangeBossState()
    {
        ChangeState(new BossIdleState(this));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerWeapon")
        {
            Debug.Log("Ouch");
            GameManager.Instance.ApplyDamage(10, this);
            UIManager.Instance.ApplyDamageToUIBoss();
        }
        if(other.gameObject.tag == "Parrying")
        {
            GameManager.Instance.SuccessParrying();

        }
    }
    public void OnAttack()
    {
        _atkCollider.enabled = true;
    }
    public void OffAttack()
    {
        _atkCollider.enabled =false;
    }
    public void ApplyDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            _currentHp = 0;
        }
    }
    public void FillMp(int amount)
    {
        

    }
    public void OnCry()
    {
        _audioSource.Play();
    }
    public void OnAtk()
    {
        _atkCollider.enabled = true;
    }
    public void OffAtk()
    {
        _atkCollider.enabled = false;
    }
}
