using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmManager : Singleton<BpmManager>
{
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;

    [SerializeField] public float timingError = 0.4f;
    private float startTime;

    [SerializeField] private float audioOffset = 0f;
    [SerializeField] public float bpm = 20f;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        _audioSource.Play();
        startTime = Time.time;
    }

    private void Update()
    {
        float beat = ((Time.time - startTime)/1000)+ audioOffset % (60 / bpm);
        if (beat<timingError||beat>(60/bpm)-timingError)
        {
            GoodBeat();
        }
    }
    
    public void GoodBeat()
    {
        Debug.Log("goodbeat");
    }
}
