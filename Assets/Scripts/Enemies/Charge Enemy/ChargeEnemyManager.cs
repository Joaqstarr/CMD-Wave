using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyManager : MonoBehaviour
{
    [SerializeField]
    private BaseEnemyData _enemyData;

    #region StateReferences
    public ChargeEnemyBaseState CurrentState { get; private set; }
    public ChargeEnemyIdleState IdleState { get; private set; } = new ChargeEnemyIdleState();
    public ChargeEnemySeekState SeekState { get; private set; } = new ChargeEnemySeekState();
    public ChargeEnemyAttackState AttackState { get; private set; } = new ChargeEnemyAttackState();
    public ChargeEnemyHitState HitState { get; private set; } = new ChargeEnemyHitState();
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
        _playerData = _player.GetComponent<PlayerSubManager>().SubData;
        _target = transform.Find("Target");

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
        CurrentState.OnUpdateState(this);
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState(this);
    }

    public void SwitchState(ChargeEnemyBaseState newState)
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
