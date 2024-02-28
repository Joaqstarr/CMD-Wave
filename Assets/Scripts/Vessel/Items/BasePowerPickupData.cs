using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePowerPickupData : ScriptableObject
{
    [Tooltip("Index of the power for use ")]
    public float powerIndex;
    public float sizeIncrease;
}
