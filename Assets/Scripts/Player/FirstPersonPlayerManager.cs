using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(FirstPersonPlayerControls))]
public class FirstPersonPlayerManager : MonoBehaviour
{
    [SerializeField]  private PlayerData _playerData;

    #region StateReferences
    public FirstPersonBaseState CurrentState {  get; private set; }
    public FirstPersonBaseState MovementState { get; private set; } = new FirstPersonMovementState();

    #endregion

    #region ComponentReferences
    public CharacterController CharacterController { get; private set; }
    public FirstPersonPlayerControls FirstPersonControls { get; private set; }

    #endregion
    void Start()
    {

        //component setup
        CharacterController = GetComponent<CharacterController>();
        FirstPersonControls = GetComponent<FirstPersonPlayerControls>();

        //state setup
        SwitchState(MovementState);

    }

    void Update()
    {
        if (CurrentState != null)
            CurrentState.OnUpdateState(this);
    }

    public void SwitchState(FirstPersonBaseState newState)
    {
        if (CurrentState != null)
            CurrentState.OnExitState(this);

        CurrentState = newState;

        CurrentState.OnEnterState(this);
    }

    public PlayerData PlayerData
    {
        get { return _playerData; }
    }
}
