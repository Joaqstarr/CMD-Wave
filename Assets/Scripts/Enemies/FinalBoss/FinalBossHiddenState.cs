using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHiddenState : BaseEnemyState
{
    private FinalBossManager manager;
     
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        if (manager == null)
            manager = (FinalBossManager)enemy;

        enemy.Pathfinder.canMove = false;
        enemy.Pathfinder.transform.localPosition = manager.StartingPos;
        enemy.Pathfinder.transform.localEulerAngles = new Vector3(0, 0, 180);
        enemy.DestinationSetter.target = null;
        manager.SplineCont.Spline.Knots = manager.StartingKnots;
        manager.SplineExtrude.enabled = false;

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
