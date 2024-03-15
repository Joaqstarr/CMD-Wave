using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerIdleState : BaseEnemyState
{
    private AnglerManager manager;
    public override void OnEnterState(BaseEnemyManager enemy)
    {

        enemy.Pathfinder.canMove = false;
        enemy.DestinationSetter.target = null;

        if (manager == null)
            manager = (AnglerManager)enemy;

        manager.ItemBait.Spawn(manager.ItemBait.RoomCode);
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if (!manager.ItemBait.gameObject.activeInHierarchy)
            enemy.SwitchState(manager.AttackState);
    }
}
