using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Dash")]
public class DashData : AbilityData
{
    public float _maxDashSpeed;
    public float _chargeTime;

    private float _dashChargeModifier = 0;
    private float _holdTime = 0;

    public override void UseAbility(GameObject player)
    {
        _holdTime += Time.deltaTime;
        if (_holdTime < _chargeTime)
        {
            _dashChargeModifier = _holdTime / _chargeTime;
        }
        else if (_holdTime > _chargeTime)
        {
            _dashChargeModifier = 1;
            _holdTime = _chargeTime;
        }

        Debug.Log(_dashChargeModifier);
    }

    public override void UseOnRelease(GameObject player)
    {
        player.GetComponent<PlayerSubManager>().StartCoroutine(Dash(player));
    }

    private IEnumerator Dash(GameObject player)
    {
        yield return new WaitForFixedUpdate();
        player.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * _maxDashSpeed * _dashChargeModifier, ForceMode.Impulse);
        _holdTime = 0;
        _dashChargeModifier = 0;
    }

}
