using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BeatManager : Singleton<BeatManager>
{
    [SerializeField] private float _bpm;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Intervals[] _intervals;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private Animator _bossAnimator;
    [SerializeField] private Animator _gunEnemyAnimator;
    [SerializeField] private Boss boss;

    private void Start()
    {
        UpdateAnimationSpeed();
        foreach (Intervals interval in _intervals)
        {
            interval.OnIntervalReached.AddListener(HandleIntervalEvent);
        }
    }

    private void Awake()
    {
        _audioSource.Play();
    }

    private void Update()
    {
        foreach (Intervals interval in _intervals)
        {
            float sampledTime = (_audioSource.timeSamples / (_audioSource.clip.frequency * interval.GetIntervalLength(_bpm)));
            interval.CheckForNewInterval(sampledTime);
        }
    }

    public void LowVolume()
    {
        _audioSource.volume = 0.2f;
    }
    public void HighVolume()
    {
        _audioSource.volume = 0.5f;
    }
    private void UpdateAnimationSpeed()
    {
        if (_animator != null)
        {
            _animator.speed = _bpm / 100f;
            _enemyAnimator.speed = _bpm / 100f;
            _bossAnimator.speed = _bpm / 100f; // 보스 애니메이터도 포함
            _gunEnemyAnimator.speed = _bpm / 100f;
        }
    }

    public void SetBPM(float bpm)
    {
        _bpm = bpm;
        UpdateAnimationSpeed();
    }

    public void ChangeBackgroundMusic(AudioClip newClip, float newBPM)
    {
        _audioSource.clip = newClip;
        _audioSource.Play();
        SetBPM(newBPM);
    }

    private void HandleIntervalEvent(float steps)
    {
        if (steps == 1f)
        {
            boss.ChangeState(new BossIdleState(boss));
            // Debug.Log("Boss animation triggered at step: " + steps);
        }
    }
}
[System.Serializable]
public class Intervals
{
    [SerializeField] private float _steps;
    [SerializeField] private UnityEvent _trigger;
    private int _lastInterval;


    public UnityEvent<float> OnIntervalReached = new UnityEvent<float>();

    public float GetIntervalLength(float bpm)
    {
        return 60f / (bpm * _steps);
    }

    public void CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != _lastInterval)
        {
            _lastInterval = Mathf.FloorToInt(interval);
            _trigger.Invoke();
            OnIntervalReached.Invoke(_steps); // 이벤트 트리거
            Debug.Log("Trigger invoked with steps: " + _steps);
        }
    }
}