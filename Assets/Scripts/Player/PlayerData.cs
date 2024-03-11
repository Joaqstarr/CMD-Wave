using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName =("PlayerData/First Person Data"))]
public class PlayerData : ScriptableObject
{
    public float MoveSpeed;
    public float LookSpeed;
    public float Gravity = -9.8f;
    [Header("Interactable")]
    public float InteractionRange;
    public LayerMask InteractionMask;

}
