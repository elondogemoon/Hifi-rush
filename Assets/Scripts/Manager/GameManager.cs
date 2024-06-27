using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ChType
{
    Player,
    Enemy,
}

public class GameManager : Singleton<GameManager>
{

    public void ApplyDamage(int damage, IDamageble character)
    {
        character.ApplyDamage(damage);
    }

}
