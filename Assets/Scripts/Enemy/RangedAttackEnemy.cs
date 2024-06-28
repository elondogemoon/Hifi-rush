using UnityEngine;
using System.Collections;
using UnityEditor;

public class RangedAttackEnemy : EnemyView
{
    public Transform attackPoint; // 공격 시작 위치
   
    public GameObject _LaserPrefab;

    //private void Start()
    //{
    //   // _LaserPrefab = Instantiate(_LaserPrefab, attackPoint.position, Quaternion.identity);
    //    _LaserPrefab.SetActive(false);
        
    //}
    public override void OnAttackState()
    {
        _LaserPrefab.SetActive(true);
    }
    public override void OnAttackEnd()
    {

       _LaserPrefab.SetActive(false);
    }


}
