using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public abstract class AbilityData : ScriptableObject
{
    public string abilityName;
    public string commandShortcut;
    public GameObject abilityPrefab;
    public int numToPool;
    public float cooldown;


    /*public virtual void UseAbility(GameObject player, GameObject ability)
    {

    }

    public virtual void UseAbility(GameObject player)
    {

    }*/
}
