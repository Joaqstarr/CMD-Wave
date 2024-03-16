using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyDeadState : BaseEnemyState
{
    private ChargeEnemyManager manager;
    private float _deathDrag = 5f;
    private float _startDrag;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        if (manager == null)
            manager = (ChargeEnemyManager)enemy;

        manager.Rb.isKinematic = false;
        enemy.Pathfinder.canMove = false;
        _startDrag = manager.Rb.drag;
        manager.Rb.drag = _deathDrag;
        manager.Rb.useGravity = true;

    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        manager.Rb.isKinematic = true;

        enemy.transform.DOComplete();
        enemy.Pathfinder.canMove = true;
        manager.Rb.drag = _startDrag;
        manager.Rb.useGravity = false;
        manager.Rb.MovePosition(manager._startPosition);
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {

    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if (Vector2.Distance(manager.Player.transform.position, manager._startPosition) > manager.Data._respawnRange)
            enemy._enemyHealth.Revive();
    }
}
