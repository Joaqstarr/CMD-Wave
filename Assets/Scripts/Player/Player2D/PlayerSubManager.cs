using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSubData _subData;
    [SerializeField]
    public SubViewCone _viewCone;

    #region StateReferences
    public SubBaseState CurrentState { get; private set; }
    public SubMovementState MovementState { get; private set; } = new SubMovementState();
    public SubStunState StunState { get; private set; } = new SubStunState();
    public SubDeathState DeathState { get; private set; } = new SubDeathState();
    #endregion

    #region ComponentReferences
    public PlayerSubControls SubControls { get; private set; }
    public Rigidbody Rb {  get; private set; }
    public PlayerSubHealth Health { get; private set; }
    #endregion

    #region Variables
    public float _stunTimer;
    #endregion

    void Start()
    {
        // set components
        SubControls = GetComponent<PlayerSubControls>();
        Rb = GetComponent<Rigidbody>();
        Health = GetComponent<PlayerSubHealth>();

        // movement state
        CurrentState = MovementState;

        // enter state
        CurrentState.OnEnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState.OnUpdateState(this);
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState(this);
    }

    public void SwitchState(SubBaseState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExitState(this);

        CurrentState = newState;

        CurrentState.OnEnterState(this);
    }

    public PlayerSubData SubData
    {
        get { return _subData; }
    }

}
