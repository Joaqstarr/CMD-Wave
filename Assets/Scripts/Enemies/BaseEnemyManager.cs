using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyManager : MonoBehaviour
{
    public BaseEnemyData BaseData { get; protected set; }
    #region StateReferences
    public BaseEnemyState CurrentState { get; private set; }

    #endregion

    public bool _dead { get; protected set; } = false;


    // Update is called once per frame
    protected void Update()
    {
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
