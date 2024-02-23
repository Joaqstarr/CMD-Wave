using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubManager : MonoBehaviour
{
    [SerializeField]
    private PlayerSubData _subData;

    #region StateReferences
    public SubBaseState CurrentState { get; private set; }
    public SubMovementState MovementState { get; private set; } = new SubMovementState();
    #endregion

    #region ComponentReferences
    public PlayerSubControls SubControls { get; private set; }
    public Rigidbody Rb {  get; private set; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // set components
        SubControls = GetComponent<PlayerSubControls>();
        Rb = GetComponent<Rigidbody>();

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
