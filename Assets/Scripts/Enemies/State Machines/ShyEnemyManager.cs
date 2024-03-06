using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemyManager : MonoBehaviour
{
    [SerializeField]
    private  BaseEnemyData _enemyData;

    #region StateReferences
    public BaseEnemyBaseState CurrentState { get; private set; }
    public ShyEnemyIdleState IdleState { get; private set; } = new ShyEnemyIdleState();
    public ShyEnemySeekState SeekState { get; private set; } = new ShyEnemySeekState();
    public ShyEnemyAttackState AttackState { get; private set; } = new ShyEnemyAttackState();
    public ShyEnemyHitState HitState { get; private set; } = new ShyEnemyHitState();
    #endregion

    #region ComponentReferences
    public Rigidbody Rb { get; private set; }
    public BaseEnemyHealth Health { get; private set; }
    public AIDestinationSetter DestinationSetter { get; private set; }
    public AIPath Pathfinder { get; private set; }
    #endregion

    #region Variables
    [HideInInspector]
    public GameObject _player;
    [HideInInspector]
    public Transform _target;
    [HideInInspector]
    public float _stunTimer;
    #endregion
    void Start()
    {
        // set components
        Rb = GetComponent<Rigidbody>();
        Health = GetComponent<BaseEnemyHealth>();
        DestinationSetter = GetComponent<AIDestinationSetter>();
        Pathfinder = GetComponent<AIPath>();

        // set variables
        _player = GameObject.FindGameObjectWithTag("PlayerSub");
        _target = transform.Find("Target");

        // movement state
        CurrentState = IdleState;

        // enter state
        CurrentState.OnEnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        CurrentState.OnUpdateState(this);
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState(this);
    }

    public void SwitchState(BaseEnemyBaseState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExitState(this);

        CurrentState = newState;

        CurrentState.OnEnterState(this);
    }

    public BaseEnemyData EnemyData
    {
        get { return _enemyData; }
    }
}
