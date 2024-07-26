using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour, IDamageble
{
    [SerializeField] public int _bossHp = 200;
    [SerializeField] private float damageCooldown = 0.5f; // 데미지 쿨다운 시간
    public int _currentHp;
    public Animator _animator;
    public NavMeshAgent _navAgent;
    public Collider _collider;
    public Collider _atkCollider;
    public AudioSource _audioSource;
    public GameObject gimmicklaser;
    public Transform gimmickTransform;
    public GameObject laser;
    private bool isDie = false;
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
        if (_currentHp <= 0&&!isDie)
        {
            _animator.SetTrigger("Die");
            StartCoroutine(BossClear());
            isDie = true;
        }
    }

    IEnumerator BossClear()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
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
            SceneManager.LoadScene(0);
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

    
    
    public void Gimmick()
    {
        if (gimmicklaser != null)
        {
            Destroy(gimmicklaser); 
        }

        gimmicklaser = Instantiate(gimmicklaser, gimmickTransform.position, Quaternion.identity);
        gimmicklaser.transform.forward = gimmickTransform.forward;
        Destroy(gimmicklaser, 5f); 
    }

    public void GimmickSuccess()
    {
        if (gimmicklaser != null)
        {
            Destroy(gimmicklaser); // 파티클을 수동으로 파괴
            gimmicklaser = null;
        }
    }
    public void GimmickFail()
    {
        if(gimmicklaser != null)
        {
            laser = Instantiate(laser, gimmickTransform.position, Quaternion.identity);
            laser.transform.forward = gimmickTransform.forward;
            Destroy(laser, 4f);
        }
    }
    public void Fight()
    {
        UIManager.Instance.StartFight();
    }
}
