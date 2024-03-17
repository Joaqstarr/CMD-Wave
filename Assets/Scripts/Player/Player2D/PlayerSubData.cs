using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("PlayerSubData/Player Sub Data"))]
public class PlayerSubData : ScriptableObject
{

    public static bool Colorblind = false;
    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velocityModifier;
    public float frictionModifier;
    //public float bounceModifier; unused currently - could be used to add bounce back from collisions

    [Header("Combat")]
    public int health;
    public float invulnTime;
    public int healthNoDrain = 10;
    public float healthDrainTickTime = 0.8f;

    [Header("View Cone")]
    public float fov;
    public int resolution;
    public float viewDistance;
    public int rayResolution;
    //public float blipGhostTime; // unnecessary? just use 1/sampleRate - more realistic/accurate
    public int sampleRate;
    public GameObject blipObject;
    public LayerMask collisionMask;
    public Material defaultColor;

    public Material enemyColor
    {
        get { return Colorblind? enemyColorColorblind : enemyColorDefault; }
    }
    public Material enemyColorDefault;
    public Material enemyColorColorblind;

    public Material radarColor
    {
        get { return Colorblind ? radarColorColorblind : radarColorDefault; }

    }
    public Material radarColorDefault;
    public Material radarColorColorblind;
    public float cameraLookAhead = 5f;
    [Header("Sound")]
    [Range(0, 1)]
    public float minimumEngineVolume;

}
