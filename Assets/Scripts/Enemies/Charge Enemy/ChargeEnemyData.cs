using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("EnemyData/Charge Enemy Data"))]
public class ChargeEnemyData : BaseEnemyData
{
    [Header("Charge Enemy Data")]
    public float chargeForceValue;
    public float chargeStartupTime;
    public float chargeEndLagTime;
    public float chargeCooldownTime;
}
