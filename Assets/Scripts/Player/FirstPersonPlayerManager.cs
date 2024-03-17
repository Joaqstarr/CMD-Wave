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
    [SerializeField]
    private Transform _headPos;

    public AudioClip[] Footsteps;
    public AudioSource FootstepSource;
    [HideInInspector]
    public IInteractable _highlightedObject;
    [SerializeField]
    private Vector3 _newGamePos;
    [SerializeField]
    private Vector3 _startPos;

    #endregion
    void Start()
    {


        //component setup
        CharacterController = GetComponent<CharacterController>();
        FirstPersonControls = GetComponent<FirstPersonPlayerControls>();
        FootstepSource = GetComponent<AudioSource>();

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

    public Transform HeadPos
    {
        get { return _headPos; }
    }

    private void NewGame()
    {
        transform.localPosition = _newGamePos;
    }
    private void ContinueGame()
    {
        transform.localPosition = _startPos;
    }
    private void OnEnable()
    {
        GameStartManager.GameStarted += NewGame;
        GameStartManager.GameContinued += ContinueGame;
    }
    private void OnDisable()
    {
        GameStartManager.GameStarted -= NewGame;
        GameStartManager.GameContinued -= ContinueGame;

    }
}
