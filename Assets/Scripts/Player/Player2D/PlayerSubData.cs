using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("PlayerSubData/Player Sub Data"))]
public class PlayerSubData : ScriptableObject
{
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velocityModifier;
    public float frictionModifier;
    public float bounceModifier;
}
