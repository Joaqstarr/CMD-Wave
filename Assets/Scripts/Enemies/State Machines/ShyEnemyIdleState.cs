using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemyIdleState : BaseEnemyBaseState
{
    public override void OnEnterState(ShyEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = false;
    }

    public override void OnExitState(ShyEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = true;
    }

    public override void OnUpdateState(ShyEnemyManager enemy)
    {

        // switch state conditionals
        
        // seek
        if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) < enemy.EnemyData.detectionRadius)
            enemy.SwitchState(enemy.SeekState);
    }

    public override void OnFixedUpdateState(ShyEnemyManager enemy)
    {

    }
}
