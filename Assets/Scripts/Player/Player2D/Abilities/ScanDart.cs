using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScanDart/Scan Dart")]
public class ScanDart : AbilityArchetype
{
    public float launchForce;

    public override void UseAbility(GameObject player, GameObject ability)
    {
        ability.transform.position = player.transform.position;
        if (!ability.activeSelf) ability.SetActive(true);
        ability.transform.parent = player.transform.Find("Abilities");
        ability.GetComponent<CapsuleCollider>().enabled = true;

        //ability.transform.rotation = new Vector3(ability.transform.rotation.x, ability.transform.rotation.y, SubViewCone.subAimAngle);
        ability.GetComponent<Rigidbody>().AddForce(launchForce * SubViewCone.subAimVector);
    }

}
