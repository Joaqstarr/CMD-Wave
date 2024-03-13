using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemyManager : MonoBehaviour
{
    [SerializeField]
    private  ShyEnemyData _enemyData;

    #region StateReferences
    public ShyEnemyBaseState CurrentState { get; private set; }
    public ShyEnemyBaseState IdleState { get; private set; } = new ShyEnemyIdleState();
    public ShyEnemyBaseState SeekState { get; private set; } = new ShyEnemySeekState();
    public ShyEnemyBaseState AttackState { get; private set; } = new ShyEnemyAttackState();
    public ShyEnemyBaseState HitState { get; private set; } = new ShyEnemyHitState();

    public ShyEnemyBaseState DeadState { get; private set; } = new ShyEnemyDeadState();
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
    public PlayerSubData _playerData;
    [HideInInspector]
    public BaseEnemyHealth _enemyHealth;
    [HideInInspector]
    public Transform _target;
    [HideInInspector]
    public float _stunTimer;

    [HideInInspector]
    public Vector2 _startPosition;

    private bool _dead = false;
    #endregion
    void Start()
    {
        // set components
        Rb = GetComponent<Rigidbody>();
        Health = GetComponent<BaseEnemyHealth>();
        DestinationSetter = GetComponent<AIDestinationSetter>();
        Pathfinder = GetComponent<AIPath>();

        _startPosition = transform.position;

        // set variables
        _player = GameObject.FindGameObjectWithTag("PlayerSub");
        _playerData = _player.GetComponent<PlayerSubManager>().SubData;
        _target = transform.Find("Target");
        _enemyHealth = GetComponent<BaseEnemyHealth>();
        // set target
        DestinationSetter.target = _target;

        // movement state
        CurrentState = IdleState;

        // enter state
        CurrentState.OnEnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        if(_enemyHealth.IsDead != _dead)
        {
            _dead = _enemyHealth.IsDead;
            if(_dead )
            {
                SwitchState(DeadState);
            }
            else
            {
                SwitchState(IdleState);
            }
        }
        CurrentState.OnUpdateState(this);

    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState(this);
    }

    public void SwitchState(ShyEnemyBaseState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExitState(this);

        CurrentState = newState;

        CurrentState.OnEnterState(this);
    }

    public ShyEnemyData EnemyData
    {
        get { return _enemyData; }
    }
}
