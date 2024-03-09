using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

//******* *CURRENTLY UNUSED ********
public class ShyEnemyAttackState : ShyEnemyBaseState
{
    public override void OnEnterState(ShyEnemyManager enemy)
    {
        Debug.Log("entering attack state");
    }

    public override void OnExitState(ShyEnemyManager enemy)
    {

    }

    public override void OnUpdateState(ShyEnemyManager enemy)
    {

        // switch state conditionals

        if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) > enemy.EnemyData.attackRadius)
            enemy.SwitchState(enemy.SeekState);
    }

    public override void OnFixedUpdateState(ShyEnemyManager enemy)
    {

    }
}
