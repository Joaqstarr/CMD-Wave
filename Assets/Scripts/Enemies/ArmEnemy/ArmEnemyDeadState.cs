using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class ArmEnemyDeadState : BaseEnemyState
{
    private ArmEnemyManager manager;

    float maxDistance = 10;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        if (manager == null)
            manager = (ArmEnemyManager)enemy;

        manager.Pathfinder.canMove = false;
        manager.DestinationSetter.target = null;
        maxDistance = manager.meshSpline.CalculateLength();

        DOVirtual.Float(maxDistance, manager.Data.DeadSplinePercent * manager.Data.MaxLength, 4f, (x) =>
        {
            float interval = x / manager.ArmParts.Length;
            for (int i = 0; i < manager.ArmParts.Length; i++)
            {

                manager.ArmParts[i].transform.localPosition = manager.meshSpline.Spline.GetPointAtLinearDistance(0, interval * i, out float dist);
            }
        });
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
