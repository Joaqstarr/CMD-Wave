using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class ArmEnemyIdleState : BaseEnemyState
{
    private ArmEnemyManager manager;

    float maxDistance = 10;

    public override void OnEnterState(BaseEnemyManager enemy)
    {
        if(manager == null) 
            manager = (ArmEnemyManager)enemy;

        manager.Pathfinder.canMove = false;
        manager.DestinationSetter.target = null;
        maxDistance = manager.meshSpline.CalculateLength();

        DOVirtual.Float(maxDistance, manager.Data.IdleSplinePercent * manager.Data.MaxLength, 1f, (x) =>
        {
            float interval = x / manager.ArmParts.Length;
            for (int i = 0; i < manager.ArmParts.Length; i++)
            {

                manager.ArmParts[i].transform.localPosition = manager.meshSpline.Spline.GetPointAtLinearDistance(0, interval * i, out float dist);
            }
        });
        //manager.meshSpline.Spline[3].
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        if (Vector3.Distance(manager.transform.position, manager.Target.transform.position) <= manager.Data.detectionRadius)
                enemy.SwitchState(manager.AttackState);

    }
}
