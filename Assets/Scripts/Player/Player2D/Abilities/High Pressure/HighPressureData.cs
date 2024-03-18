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
    public float maxTimer = 4f;
    private float timer =0 ;
    public override void UseAbility(GameObject player, GameObject ability)
    {

        if(CalculateTime() < maxTimer)
        {
            CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Pressure Blast Charging. Time Left: " + (4-CalculateTime()).ToString("F2") +" Seconds"), false);

            return;
        }


        poolObjects[0].SetActive(true);
        poolObjects[0].GetComponent<HighPressureBlast>().StartBlast();
        poolObjects[0].GetComponent<HighPressureBlast>().RechargeBlast();

        timer = Time.time;
    }

    public override void OnActivationFailed()
    {
        Debug.Log("refilling");
        CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Pressure Blast Depleted. Manual Recharge Necessary."), false);
         // remove when room refill is added
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

    private float CalculateTime()
    {
        float newTime = Time.time - timer;

        if (Mathf.Abs( newTime) > 10)
        {
            timer = Time.time - maxTimer;
            newTime = Time.time - timer;
        }

        return newTime;
    }
}
