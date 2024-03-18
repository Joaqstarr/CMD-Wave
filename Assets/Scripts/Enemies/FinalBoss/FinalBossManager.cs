using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FinalBossManager : BaseEnemyManager
{
    public BaseEnemyData _data;
    public SplineContainer SplineCont;
    [HideInInspector]
    public SplineInstantiate SplineExtrude;
    public Transform MovePoint;
    public float MaxDistance = 15f;

    public int MaxSplineLength = 10;
    public BezierKnot[] StartingKnots;

    public BaseEnemyState AttackState = new FinalBossAttackState();
    private float _stunTime;
    private bool stunned = false;
    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        IdleState = new FinalBossHiddenState();
        DeadState = new FinalBossDeadState();
        SplineExtrude = GetComponentInChildren<SplineInstantiate>();
        
        base.Start();
        _data = _enemyHealth._enemyData;
        StartingKnots = SplineCont.Spline.ToArray();
        Pathfinder.maxSpeed = _data.speed;
        SwitchState(AttackState);
    }


    public void StartBoss()
    {
        if(CurrentState == IdleState)
        {
            SwitchState(AttackState);
        }
    }

    private void Update()
    {
        base.Update();

        if (stunned && CurrentState == AttackState)
        {
            if(_stunTime < 0)
            {
                Pathfinder.canMove = true;
                stunned = false;
            }
            else
            {
                Pathfinder.canMove = false;
                _stunTime -= Time.deltaTime;
            }
        }
    }
    public void Hit()
    {
        stunned = true;
        _stunTime += _data.stunDuration;
        Pathfinder.transform.DOKill();
        Pathfinder.transform.DOMove(Pathfinder.transform.position + (Pathfinder.transform.position - Target.position).normalized * _data.knockbackValue, _stunTime).SetEase(Ease.OutSine);
    }


}
