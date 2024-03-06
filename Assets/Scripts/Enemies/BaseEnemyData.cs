using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("BaseEnemyData/Base Enemy Data"))]
public class BaseEnemyData : ScriptableObject
{
    [Header("General")]
    public float detectionRadius;

    [Header("Movement")]
    public float speed;
    public float acceleration;

    [Header("Combat")]
    public float health;
    public float damage;
    public float knockbackValue;
    public float stunDuration;
}
