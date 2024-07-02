using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmManager : Singleton<BpmManager>
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;

    [SerializeField] public float timingError = 0.1f; // 타이밍 윈도우 축소
    private float startTime;

    [SerializeField] private float audioOffset = 0f;
    [SerializeField] public float bpm = 120f; // BPM 값을 일반적인 값으로 설정했습니다.

    private float lastBeatTime = -Mathf.Infinity; // 마지막으로 비트가 발생한 시간을 저장

    private void Awake()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void Start()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        startTime = Time.time;
    }

    private void Update()
    {
        float timeElapsed = Time.time - startTime;
        float beatDuration = 60 / bpm;
        float currentTime = (timeElapsed + audioOffset) % beatDuration;

        if ((currentTime < timingError || currentTime > beatDuration - timingError) && Time.time - lastBeatTime > timingError)
        {
            lastBeatTime = Time.time; // 마지막 비트 시간을 업데이트
            GoodBeat();
        }
    }

    public void GoodBeat()
    {
        Debug.Log("goodbeat");
    }
}
