using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FinalBossManager : BaseEnemyManager, IDataPersistance
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

    public static bool BossStarted = false;

    [HideInInspector]
    public Vector3 StartingPos;
    [HideInInspector]
    public AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        IdleState = new FinalBossHiddenState();
        DeadState = new FinalBossDeadState();
        SplineExtrude = GetComponentInChildren<SplineInstantiate>();
        base.Start();
        StartingPos = Pathfinder.transform.localPosition;
        _source = GetComponent<AudioSource>();
        _data = _enemyHealth._enemyData;
        StartingKnots = SplineCont.Spline.ToArray();
        Pathfinder.maxSpeed = _data.speed;
        SwitchState(IdleState);
    }


    public void StartBoss()
    {
        if(CurrentState == IdleState)
        {
            BossStarted = true;

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

    public void SaveData(ref SaveData data)
    {
    }

    public void LoadData(SaveData data)
    {
        BossStarted = false;
        _enemyHealth.Invulnerable = true;
        SwitchState(IdleState);

    }
    private void OnEnable()
    {
        GameStartManager.GameContinued += ResetBool;
        GameStartManager.GameStarted += ResetBool;

    }
    private void OnDisable()
    {
        GameStartManager.GameContinued -= ResetBool;
        GameStartManager.GameStarted -= ResetBool;

    }
    private void ResetBool()
    {
        BossStarted = false;
        _enemyHealth.Invulnerable = true;

    }
}
