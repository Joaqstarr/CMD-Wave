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
        Debug.Log("data script");
        poolObjects[0].SetActive(true);
        poolObjects[0].GetComponent<HighPressureBlast>().StartBlast();
    }

    public override void OnActivationFailed()
    {
        poolObjects[0].GetComponent<HighPressureBlast>().RechargeBlast();
    }

    public override GameObject GetAbilityObject()
    {
        if (poolObjects[0].GetComponent<HighPressureBlast>()._hasBlast)
        {
            Debug.Log("got particle");
            return poolObjects[0];
        }
        return null;
    }
}
