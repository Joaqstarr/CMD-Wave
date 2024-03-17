using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class ArmEnemyAttackState : BaseEnemyState
{
    private ArmEnemyManager manager;
    public float maxDistance;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        if (manager == null)
            manager = (ArmEnemyManager)enemy;
        if (manager.canMove)
        {
            manager.Pathfinder.canMove = true;
            manager.DestinationSetter.target = manager.Target;
        }
        else
        {
            manager.Pathfinder.canMove = false;
            manager.DestinationSetter.target = null;
        }


        float start = 0;
        maxDistance = start;
        DOVirtual.Float(start, manager.Data.AttackSplinePercent * manager.Data.MaxLength, 1.3f, (x) =>
        {
            maxDistance = x ;
        });
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        manager.Pathfinder.canMove = false;

    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {

    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {

        manager.SetKnotPosition(manager.meshSpline.Spline.Count-1, Vector3.ClampMagnitude(manager.DestinationSetter.transform.position - enemy.transform.position, manager.Data.MaxLength));

        for (int i = manager.meshSpline.Spline.Count-2; i > 1; i--)
        {
            Vector3 midpoint = manager.meshSpline.Spline[i+1].Position;
            midpoint *= manager.Data.MidPointPercentage;

            manager.SetKnotPosition(i, midpoint);
        }


        float interval = maxDistance / manager.ArmParts.Length;
        for (int i = 0; i < manager.ArmParts.Length; i++)
        {

            manager.ArmParts[i].transform.localPosition = manager.meshSpline.Spline.GetPointAtLinearDistance(0,interval * i, out float dist);
        }

        if (Vector3.Distance(manager.transform.position, manager.Target.transform.position) > manager.Data.detectionRadius)
            enemy.SwitchState(manager.IdleState);
    }


}
