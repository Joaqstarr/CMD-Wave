using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(menuName = "AbilityData/Ability Data")]
public abstract class AbilityData : ScriptableObject
{
    public string abilityName;
    public string commandShortcut;
    public GameObject abilityPrefab;
    public int numToPool;
    [HideInInspector]
    public GameObject[] poolObjects;
    public float cooldown;

    public virtual void OnStart()
    {

    }
    public virtual void UseAbility(GameObject player, GameObject ability)
    {

    }

    public virtual void UseAbility(GameObject player)
    {

    }

    public virtual void UseOnRelease(GameObject player) 
    {
    
    }

    public virtual void OnActivationFailed()
    {

    }
    public virtual void OnActivationFailed(GameObject player)
    {

    }

    public virtual GameObject GetAbilityObject()
    {
        return null;
    }
}
