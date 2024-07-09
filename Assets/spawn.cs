using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SpawnManager.Instance.SpawnEnemy("sword",SpawnManager.Zone.Zone1);
            SpawnManager.Instance.SpawnEnemy("sword", SpawnManager.Zone.Zone1);
            SpawnManager.Instance.SpawnEnemy("sword", SpawnManager.Zone.Zone1);
            
        }
    }
}
