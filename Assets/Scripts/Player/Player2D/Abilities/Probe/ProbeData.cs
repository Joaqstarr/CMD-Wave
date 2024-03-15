using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Probe")]
public class ProbeData : AbilityData
{

    private bool _isControlling;

    public override void UseAbility(GameObject player, GameObject ability)
    {
        Probe probe = ability.GetComponent<Probe>();
        probe.gameObject.SetActive(true);
        FirstPersonPlayerControls.Instance.Possess(ability.GetComponent<Probe>()._probeControls, false);
        _isControlling = true;
        probe.transform.position = PlayerSubControls.Instance.transform.position;

        probe._mapVirtualCamera.Priority = 11;
    }

    public override void OnActivationFailed()
    {
        FirstPersonPlayerControls.Instance.UnPossess(poolObjects[0].GetComponent<Probe>()._probeControls);
        poolObjects[0].GetComponent<Probe>()._mapVirtualCamera.Priority = 0;
        _isControlling = false;
        poolObjects[0].SetActive(false);

    }
    public override GameObject GetAbilityObject()
    {
        if (poolObjects[0].activeInHierarchy)
            return poolObjects[0];
        return null;
    }

}
