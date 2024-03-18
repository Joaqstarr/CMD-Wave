using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyManager : MonoBehaviour
{
    
    public BaseEnemyData BaseData { get; protected set; }
    #region StateReferences
    public BaseEnemyState CurrentState { get; private set; }
    public BaseEnemyState IdleState;

    public BaseEnemyState DeadState;

    #endregion

    public bool _dead { get; protected set; } = false;

    public Transform Target;
    [HideInInspector]
    public AIPath Pathfinder { get; private set; }
    [HideInInspector]
    public AIDestinationSetter DestinationSetter { get; private set; }
    public BaseEnemyHealth _enemyHealth { get; private set; }
    protected void Start()
    {
        Target = GameObject.Find("SubPlayer").transform;
        Pathfinder = GetComponentInChildren<AIPath>();
        DestinationSetter = GetComponentInChildren<AIDestinationSetter>();
        _enemyHealth = GetComponent<BaseEnemyHealth>();
        

    }
    // Update is called once per frame
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
        if (CurrentState != null)
            CurrentState.OnUpdateState(this);
    }
    private void FixedUpdate()
    {
        if (CurrentState != null)
            CurrentState.OnFixedUpdateState(this);
    }

    public void SwitchState(BaseEnemyState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExitState(this);

        CurrentState = newState;

        CurrentState.OnEnterState(this);

    }

}
