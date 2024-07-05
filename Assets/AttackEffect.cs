using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] private GameObject hitEffectPrefab; // 피격 효과 프리팹

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // 피격 효과 생성
            Instantiate(hitEffectPrefab, other.transform.localPosition, Quaternion.identity);

            
        }
    }
}

