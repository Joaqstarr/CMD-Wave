using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnglerDeadState : BaseEnemyState
{
    private AnglerManager manager;

    public override void OnEnterState(BaseEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = false;
        enemy.DestinationSetter.target = null;

        if (manager == null)
            manager = (AnglerManager)enemy;

        manager.Rb.useGravity = true;
        manager.Rb.isKinematic = false;

    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        manager.Rb.useGravity = false;
        manager.Rb.isKinematic = true;
        manager.Rb.MovePosition(manager.StartLocation);
        manager.ItemBait.Spawn(manager.ItemBait.RoomCode);
        manager.ItemBait.GetComponent<Rigidbody>().MovePosition(manager.StartItemLocation);

    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if (Vector2.Distance(enemy.Target.position, manager.StartLocation) > enemy.BaseData._respawnRange)
            enemy._enemyHealth.Revive();
    }
}
