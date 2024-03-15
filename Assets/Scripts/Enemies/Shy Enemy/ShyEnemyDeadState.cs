using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemyDeadState : ShyEnemyBaseState
{
    private float _deathDrag = 5f;
    private float _startDrag;
    public override void OnEnterState(ShyEnemyManager enemy)
    {
        enemy.Rb.isKinematic = false;
        enemy.Pathfinder.canMove = false;
        _startDrag = enemy.Rb.drag;
        enemy.Rb.drag = _deathDrag;
        enemy.Rb.useGravity = true;

    }

    public override void OnExitState(ShyEnemyManager enemy)
    {
        enemy.Rb.isKinematic = true;

        enemy.transform.DOComplete();
        enemy.Pathfinder.canMove = true;
        enemy.Rb.drag = _startDrag;
        enemy.Rb.useGravity = false;
        enemy.Rb.MovePosition(enemy._startPosition);
    }

    public override void OnFixedUpdateState(ShyEnemyManager enemy)
    {

    }

    public override void OnUpdateState(ShyEnemyManager enemy)
    {
        if(Vector2.Distance( enemy._player.transform.position, enemy._startPosition)>enemy.EnemyData._respawnRange)
            enemy._enemyHealth.Revive();
    }
}
