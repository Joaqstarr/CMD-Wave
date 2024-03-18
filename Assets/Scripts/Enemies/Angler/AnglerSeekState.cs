using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerSeekState : BaseEnemyState
{
    private AnglerManager manager;

    public override void OnEnterState(BaseEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = false;
        enemy.DestinationSetter.target = enemy.Target;

        if (manager == null)
            manager = (AnglerManager)enemy;


        if (!manager.ItemBait.gameObject.activeInHierarchy)
        {

            manager.Rb.isKinematic = false;

            Vector3 direction = enemy.Target.position - enemy.transform.position;
            direction.Normalize();

            manager.Rb.AddForce(direction * enemy.BaseData.speed, ForceMode.Impulse);
            manager._source.Play();
        }
        else
        {
            enemy.Pathfinder.canMove = true;
            manager.ItemBait.Collect();

        }

    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        manager.Rb.isKinematic = true;

    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if(manager.Rb.velocity.magnitude < 0.5f && enemy.Pathfinder.canMove == false)
        {
            manager.Rb.isKinematic = true;
            enemy.Pathfinder.canMove = true;

        }
    }
}
