using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FinalBossAttackState : BaseEnemyState
{
    private FinalBossManager manager;
    public override void OnEnterState(BaseEnemyManager enemy)
    {

        if(manager == null)
            manager = (FinalBossManager)enemy;

        manager._enemyHealth.Invulnerable = false;
        manager.SplineExtrude.enabled = true;
        enemy.DestinationSetter.target = enemy.Target;
        enemy.Pathfinder.canMove = true;
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
        BezierKnot tempKnot = manager.SplineCont.Spline[manager.SplineCont.Spline.Count - 1];
        tempKnot.Position = manager.MovePoint.localPosition;
        float length = manager.SplineCont.Spline.GetCurveLength(manager.SplineCont.Spline.Count - 2);
        
        if (length > manager.MaxDistance)
        {
            manager.SplineCont.Spline.Add(tempKnot);
        }
        else
        {
            manager.SplineCont.Spline.SetKnot(manager.SplineCont.Spline.Count - 1, tempKnot);

        }
        if (manager.SplineCont.Spline.Count > manager.MaxSplineLength)
        {
            manager.SplineCont.Spline.RemoveAt(0);
        }
        manager.SplineCont.Spline.SetTangentMode(TangentMode.AutoSmooth);

    }
}
