using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyAttackState : BaseEnemyState
{
    private ChargeEnemyManager manager;
    private Vector3 chargeAngle;
    private bool charging;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        Debug.Log("Enter attack state");
        if (manager == null)
            manager = (ChargeEnemyManager)enemy;

        enemy.Pathfinder.canMove = false;

        manager.Rb.isKinematic = false;
        manager.Rb.velocity = Vector3.zero;

        chargeAngle = (manager.Player.transform.position - enemy.transform.position) * 100;
        chargeAngle = chargeAngle.normalized;

        charging = true;

        enemy.StartCoroutine(Charge());
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        enemy.DestinationSetter.target = manager.Player.transform;
        manager.chargeCooldownTimer = manager.Data.chargeCooldownTime;
        manager.Rb.isKinematic = true;
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {

        // switch state conditionals

        // seek
        if (charging == false)
            enemy.SwitchState(manager.SeekState);
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {

    }

    private IEnumerator Charge()
    {
        manager.Roar.Play();
        yield return new WaitForSeconds(manager.Data.chargeStartupTime);
        yield return new WaitForFixedUpdate();
        manager.Pathfinder.canMove = true;
        Debug.Log(chargeAngle * manager.Data.chargeForceValue);
        manager.Rb.AddForce(chargeAngle * manager.Data.chargeForceValue, ForceMode.Impulse);


        yield return new WaitForSeconds(manager.Data.chargeEndLagTime);
        charging = false;
    }
}
