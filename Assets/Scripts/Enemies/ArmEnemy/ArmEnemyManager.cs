using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class ArmEnemyManager : BaseEnemyManager
{

    public ArmEnemyData Data;

    #region State References

    public BaseEnemyState AttackState = new ArmEnemyAttackState();

    #endregion

    #region Component References

    [HideInInspector]
    public SplineContainer meshSpline;


    public EnemyCollision ArmPart;
    public EnemyCollision[] ArmParts;

    public bool canMove = true;

    #endregion
    private void Start()
    {
        IdleState = new ArmEnemyIdleState();
        DeadState = new ArmEnemyDeadState();
        base.Start();
        BaseData = Data;
        meshSpline = GetComponent<SplineContainer>();

        BezierKnot[] knots = new BezierKnot[4];
        knots[0] = new BezierKnot(-Vector3.up);
        knots[1] = new BezierKnot(Vector3.zero);
        knots[2] = new BezierKnot(Vector3.zero);
        knots[3] = new BezierKnot(Vector3.zero);

        meshSpline.AddSpline(new Spline(knots, false));



        ArmParts = new EnemyCollision[Data.ColliderAmount];
        ArmParts[0] = ArmPart;
        ArmParts[0].Manager = this;

        for (int i = 1; i < ArmParts.Length; i++)
        {
            ArmParts[i] = Instantiate(ArmPart, transform);
            ArmParts[i].Manager = this;
        }

        SwitchState(IdleState);
        


    }

    public void SetKnotPosition(int index, Vector3 position)
    {
        BezierKnot newKnot = meshSpline.Spline[index];
        newKnot.Position = position;
        meshSpline.Spline.SetKnot(index, newKnot);
    }
}
