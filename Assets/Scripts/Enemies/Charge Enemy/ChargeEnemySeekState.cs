using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemySeekState : BaseEnemyState
{
    private ChargeEnemyManager manager;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        Debug.Log("enter seek state");
        enemy.Pathfinder.canMove = true;

        if (manager == null)
            manager = (ChargeEnemyManager)enemy;

        manager.DestinationSetter.target = enemy.Target;
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

        // idle
        if (Vector3.Distance(enemy.transform.position, manager.Player.transform.position) > manager.Data.detectionRadius)
            enemy.SwitchState(manager.IdleState);
        //else if (Physics.Linecast(enemy.transform.position, manager.Player.transform.position, 9))
        //enemy.SwitchState(manager.IdleState);

        // attack
        if (manager.chargeCooldownTimer <= 0)
            if (Vector3.Distance(enemy.transform.position, manager.Player.transform.position) <= manager.Data.attackRadius)
                enemy.SwitchState(manager.AttackState);

    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {

    }
}
