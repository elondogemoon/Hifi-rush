using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PocketmonManager : Singleton<PocketmonManager>
{
    
    [SerializeField]
    string value;
    [SerializeField]
    GameObject Prefab_Metamong;
    public Dictionary<string,GameObject> _pocketmonDic = new Dictionary<string,GameObject>();

    private void Awake()
    {
        
        Instantiate(Prefab_Metamong,this.gameObject.transform,true);
        _pocketmonDic.Add(value, Prefab_Metamong);
    }

    public GameObject GetPocketMon(string key)
    {
        if (_pocketmonDic.ContainsKey(key))
        {

            //GameObject obj = GetComponent<Metamong>().gameObject;
            //_pocketmonDic.TryGetValue("Metamong", out var value);
            return _pocketmonDic[key];
        }
        return null;
    }


    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

   
}
