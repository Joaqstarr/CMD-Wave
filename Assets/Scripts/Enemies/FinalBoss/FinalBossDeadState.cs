using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDeadState : BaseEnemyState
{
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = false;
        enemy.DestinationSetter.target = null;

    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
    }
}
