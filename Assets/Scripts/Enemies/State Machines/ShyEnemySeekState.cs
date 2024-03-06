using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemySeekState : BaseEnemyBaseState
{
    public override void OnEnterState(ShyEnemyManager enemy)
    {
        Debug.Log("Seeking");
        enemy.DestinationSetter.target = enemy._player.transform;
    }

    public override void OnExitState(ShyEnemyManager enemy)
    {

    }

    public override void OnUpdateState(ShyEnemyManager enemy)
    {
        // switch state conditionals

        // idle
        if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) > enemy.EnemyData.detectionRadius)
            enemy.SwitchState(enemy.IdleState);
    }

    public override void OnFixedUpdateState(ShyEnemyManager enemy)
    {

    }
}
