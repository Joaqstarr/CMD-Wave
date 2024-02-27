using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("PlayerSubData/Player Sub Data"))]
public class PlayerSubData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velocityModifier;
    public float frictionModifier;
    public float bounceModifier;

    [Header("View Cone")]
    public float fov;
    public int resolution;
    public float viewDistance;
    public GameObject blip;
}
