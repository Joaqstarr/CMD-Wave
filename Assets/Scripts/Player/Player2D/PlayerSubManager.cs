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
    public SubBaseState MovementState { get; private set; } = new SubMovementState();
    public SubBaseState StunState { get; private set; } = new SubStunState();
    public SubBaseState DeathState { get; private set; } = new SubDeathState();
    public SubBaseState StartState { get; private set; } = new PlayerSubStartGameState();
    #endregion

    #region ComponentReferences
    public PlayerSubControls SubControls { get; private set; }
    public Rigidbody Rb {  get; private set; }
    public PlayerSubHealth Health { get; private set; }
    public AudioSource EngineSource ;
    public Transform CamFollow;
    public AudioSource DashSource;
    public SubViewCone SubViewCone { get; private set; }
    #endregion

    #region Variables
    public float _stunTimer;

    [Header("Game Start settings")]

    public TweenData _startTween;
    public Vector3 _newGamePos;
    public Vector3 _firstMapPos;
    #endregion

    void Start()
    {
        // set components
        SubControls = GetComponent<PlayerSubControls>();
        Rb = GetComponent<Rigidbody>();
        Health = GetComponent<PlayerSubHealth>();
        SubViewCone = GetComponentInChildren<SubViewCone>();
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

    private void OnEnable()
    {
        GameStartManager.GameStarted += NewGame;

    }
    private void OnDisable()
    {
        GameStartManager.GameStarted -= NewGame;
    }
    private void NewGame()
    {
        SwitchState(StartState);
    }

}
