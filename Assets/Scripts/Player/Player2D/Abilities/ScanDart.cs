using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScanDart/Scan Dart")]
public class ScanDart : AbilityData
{
    public float launchForce;
    public LayerMask collideWith;

    /*public override void UseAbility(GameObject player, GameObject ability)
    {
        Debug.Log("fire");
        ability.transform.position = player.transform.position;
        if (!ability.activeSelf) ability.SetActive(true);
        ability.transform.parent = player.transform.Find("Abilities");
        ability.GetComponent<CapsuleCollider>().enabled = true;

        ability.transform.Rotate(new Vector3(0, 0, SubViewCone.subAimAngle - (ability.transform.rotation.eulerAngles.z) - 90));
        ability.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }*/

}
