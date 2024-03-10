using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public abstract class AbilityArchetype : ScriptableObject
{
    public string commandShortcut;
    public float cooldown;

    public virtual void UseAbility(GameObject player, GameObject ability)
    {

    }

    public virtual void UseAbility(GameObject player)
    {

    }
}
