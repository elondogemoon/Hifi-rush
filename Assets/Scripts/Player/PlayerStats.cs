using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _maxHp = 100;
    private int _currentHp;

    private int _maxMp = 100;
    private int _currentMp;

    private void Awake()
    {
        _currentHp = _maxHp;
        _currentMp = _maxMp;
    }


    public void ReadyForSkill()
    {
        //UIManager.Instance.
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyWeapon")
        {
           
        }
    }
}
