using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyData/Arm Enemy Data")]
public class ArmEnemyData : BaseEnemyData
{

    [Header("Spline Stuff")]
    [Range(0,1)]
    public float IdleSplinePercent = 0.1f;
    [Range(0, 1)]
    public float AttackSplinePercent = 1f;
    [Range(0, 1)]
    public float DeadSplinePercent = 0.01f;
    public float MaxLength = 10f;
    public Vector2 MidPointPercentage;
    public int ColliderAmount = 5;

}
