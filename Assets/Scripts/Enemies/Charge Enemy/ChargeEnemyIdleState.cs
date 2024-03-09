using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyIdleState : ChargeEnemyBaseState
{
    public override void OnEnterState(ChargeEnemyManager enemy)
    {
        Debug.Log("Idling");
        enemy.Pathfinder.canMove = false;
    }

    public override void OnExitState(ChargeEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = true;
    }

    public override void OnUpdateState(ChargeEnemyManager enemy)
    {

        // switch state conditionals

        // seek
        if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy.EnemyData.detectionRadius)
            if (!Physics.Linecast(enemy.transform.position, enemy._player.transform.position, 9))
                enemy.SwitchState(enemy.SeekState);
    }

    public override void OnFixedUpdateState(ChargeEnemyManager enemy)
    {

    }
}

