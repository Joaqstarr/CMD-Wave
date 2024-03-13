using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyData : ScriptableObject
{
    [Header("General")]
    public float detectionRadius;
    public float attackRadius;
    public float _respawnRange = 50f;

    [Header("Movement")]
    public float speed;
    public float acceleration;

    [Header("Combat")]
    public int health;
    public float damage;
    public float knockbackValue;
    public float stunDuration;

}
