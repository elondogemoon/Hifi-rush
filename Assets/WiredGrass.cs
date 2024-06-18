using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WiredGrass : MonoBehaviour
{
    private PocketmonManager _pocketmonManager;
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _pocketmonManager = GetComponent<PocketmonManager>();
            CallMeta(_pocketmonManager);
        }
    }

    public void CallMeta(PocketmonManager pocketmonManager)
    {
        var obj = PocketmonManager.Instance.GetPocketMon("Metamong");
        var metamon = obj.GetComponent<Metamong>();
        metamon.MetaMeta();
    }
}
