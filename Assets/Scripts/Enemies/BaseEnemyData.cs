using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyData : ScriptableObject
{
    [Header("General")]
    public float detectionRadius;
    public float attackRadius;

    [Header("Movement")]
    public float speed;
    public float acceleration;

    [Header("Combat")]
    public float health;
    public float damage;
    public float knockbackValue;
    public float stunDuration;

}
