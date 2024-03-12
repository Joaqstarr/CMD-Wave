using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

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


    public virtual void UseAbility(GameObject player, GameObject ability)
    {

    }

    public virtual void UseAbility(GameObject player)
    {

    }

    public virtual void OnActivationFailed()
    {

    }
}
