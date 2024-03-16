using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyIdleState : BaseEnemyState
{
    private ChargeEnemyManager manager;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = false;
        enemy.DestinationSetter.target = null;

        if (manager == null)
            manager = (ChargeEnemyManager)enemy;
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = true;
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if (manager.chargeCooldownTimer > 0)
            manager.chargeCooldownTimer -= Time.deltaTime;

        // switch state conditionals

        // seek
        if (Vector3.Distance(enemy.transform.position, manager.Player.transform.position) <= manager.Data.detectionRadius)
            if (!Physics.Linecast(enemy.transform.position, manager.Player.transform.position, 9))
                enemy.SwitchState(manager.SeekState);
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {

    }
}

