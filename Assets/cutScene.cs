using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class cutScene : MonoBehaviour
{
    private PlayableDirector pd;
    public TimelineAsset[] ta;
    public Animator animator;
    public GameObject[] explosionObjects; // 폭발할 오브젝트들
    public GameObject explosionPrefab; // 폭발 파티클 프리팹

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CutSceen")
        {
            other.gameObject.SetActive(false);
            pd.Play(ta[0]);
            Invoke(nameof(PlayExplosions), 1f); // 1초 후 폭발 시작
            Invoke(nameof(PlayBoss), 7.8f); // 7.8초 후 보스 시작
        }
    }

    public void PlayExplosions()
    {
        StartCoroutine(TriggerExplosions());
    }

    private IEnumerator TriggerExplosions()
    {
        int explosionCount = 0;

        foreach (GameObject obj in explosionObjects)
        {
            Instantiate(explosionPrefab, obj.transform.position, Quaternion.identity);
            Destroy(obj);
            explosionCount++;

            if (explosionCount % 4 == 0)
            {
                yield return new WaitForSeconds(1.5f); // 4번 폭발 후 1초 대기
            }
            else
            {
                yield return new WaitForSeconds(0.5f); // 각 폭발 사이의 간격 (0.5초)
            }
        }
    }

    public void PlayBoss()
    {
        animator.enabled = true;
    }
}
