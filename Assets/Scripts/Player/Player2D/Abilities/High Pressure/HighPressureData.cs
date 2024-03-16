using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/High Pressure")]
public class HighPressureData : AbilityData
{
    public float blastRadius;
    public float blastDuration;
    public float knockback;
    public float stunDuration;

    public override void UseAbility(GameObject player, GameObject ability)
    {
        poolObjects[0].SetActive(true);
        poolObjects[0].GetComponent<HighPressureBlast>().StartBlast();
    }

    public override void OnActivationFailed()
    {
        Debug.Log("refilling");
        poolObjects[0].GetComponent<HighPressureBlast>().RechargeBlast();
    }

    public override GameObject GetAbilityObject()
    {
        Debug.Log(poolObjects[0].GetComponent<HighPressureBlast>()._hasBlast);
        if (poolObjects[0].GetComponent<HighPressureBlast>()._hasBlast)
        {
            return poolObjects[0];
        }
        return null;
    }
}
