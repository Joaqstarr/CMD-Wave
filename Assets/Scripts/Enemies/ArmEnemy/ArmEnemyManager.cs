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
    public BaseEnemyState IdleState = new ArmEnemyIdleState();
    public BaseEnemyState DeadState = new ArmEnemyDeadState();

    #endregion

    #region Component References

    [HideInInspector]
    public SplineContainer meshSpline;
    [HideInInspector]
    public AIPath Pathfinder { get; private set; }
    [HideInInspector]
    public AIDestinationSetter DestinationSetter { get; private set; }

    public Transform Target;
    public EnemyCollision ArmPart;
    public EnemyCollision[] ArmParts;
    public BaseEnemyHealth _enemyHealth { get; private set; }

    #endregion
    private void Start()
    {
        Target = GameObject.Find("SubPlayer").transform;

        BaseData = Data;
        Pathfinder = GetComponentInChildren<AIPath>();
        DestinationSetter = GetComponentInChildren<AIDestinationSetter>();
        meshSpline = GetComponent<SplineContainer>();
        _enemyHealth = GetComponent<BaseEnemyHealth>();

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
    protected void Update()
    {
        if (_enemyHealth.IsDead != _dead)
        {
            _dead = _enemyHealth.IsDead;
            if (_dead)
            {
                SwitchState(DeadState);
            }
            else
            {
                SwitchState(IdleState);
            }
        }
        base.Update();
    }
    public void SetKnotPosition(int index, Vector3 position)
    {
        BezierKnot newKnot = meshSpline.Spline[index];
        newKnot.Position = position;
        meshSpline.Spline.SetKnot(index, newKnot);
    }
}
