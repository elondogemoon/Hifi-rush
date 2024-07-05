using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IDamageble
{
    public int _currentHp;
    [SerializeField] public int _bossHp = 200;
    [SerializeField] public Animator _animator;
    [SerializeField] public NavMeshAgent _navAgent;
    [SerializeField] public Collider _collider;
    [SerializeField] public Collider _atkCollider;
    [SerializeField] public AudioSource _audioSource;
    [SerializeField] private float damageCooldown = 0.5f; // 데미지 쿨다운 시간
    [SerializeField] public GameObject gimmicklaser;
    [SerializeField] public Transform gimmickTransform;

    private IBossState _bossState;
    public Transform _target;
    private Dictionary<Collider, float> lastDamageTime = new Dictionary<Collider, float>();
    private bool hasEnteredGimmickState = false; 

    private void Start()
    {
        _currentHp = _bossHp;
        ChangeState(new BossAppearState(this));
        _animator = GetComponent<Animator>();
    }

    public virtual void ChangeState(IBossState newState)
    {
        _bossState?.ExitState();
        _bossState = newState;
        _bossState.EnterState();
    }

    private void Update()
    {
        // 체력이 50 이하이고, 아직 기믹 상태로 변경된 적이 없는 경우
        if (_currentHp <= 50 && !hasEnteredGimmickState)
        {
            hasEnteredGimmickState = true;
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
            float currentTime = Time.time;

            if (lastDamageTime.ContainsKey(other))
            {
                if (currentTime - lastDamageTime[other] >= damageCooldown)
                {
                    ApplyDamageToBoss(other);
                    lastDamageTime[other] = currentTime;
                }
            }
            else
            {
                ApplyDamageToBoss(other);
                lastDamageTime[other] = currentTime;
            }
        }
        if (other.gameObject.tag == "Parrying")
        {
            GameManager.Instance.SuccessParrying();
            _atkCollider.enabled = false;
        }
    }

    private void ApplyDamageToBoss(Collider other)
    {
        Debug.Log("Ouch");
        GameManager.Instance.ApplyDamage(10, this);
        UIManager.Instance.ApplyDamageToUIBoss();
    }

    public void OnAttack()
    {
        _atkCollider.enabled = true;
    }

    public void OffAttack()
    {
        _atkCollider.enabled = false;
    }

    public void ApplyDamage(int damage)
    {
        _currentHp -= damage;
        if (_currentHp <= 0)
        {
            _currentHp = 0;
            // Add logic for boss death if needed
        }
    }

    public void FillMp(int amount)
    {
        // Implement MP fill logic if needed
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
    public void CrawAttack()
    {

    }
    public void Gimmick()
    {
        GameObject damageParticle = Instantiate(gimmicklaser, gimmickTransform.position, Quaternion.identity);
        damageParticle.transform.forward = gimmickTransform.forward;
    }
    public void GimmickSuccess()
    {
        Destroy(gimmicklaser);
    }
}
