using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SubStunState : SubBaseState
{
    private PlayerSubData _subData;
    private PlayerSubControls _subControls;
    private Rigidbody _rb;
    private PlayerSubHealth _health;

    public override void OnEnterState(PlayerSubManager player)
    {
        #region Global Variable Setup
        if (_subControls == null)
            _subControls = player.SubControls;
        if (_subData == null)
            _subData = player.SubData;
        if (_rb == null)
            _rb = player.Rb;
        if (_health == null) 
            _health = player.Health;
        #endregion
    }

    public override void OnExitState(PlayerSubManager player)
    {

    }

    public override void OnUpdateState(PlayerSubManager player)
    {
        player._stunTimer -= Time.deltaTime;

        // switch state conditions

        // movement state
        if ( player._stunTimer < 0)
        {
            player.SwitchState(player.MovementState);
        }
    }

    public override void OnFixedUpdateState(PlayerSubManager player)
    {
        // draw view cone
        player._viewCone.DrawViewCone(player.SubControls.AimInput);
    }
}
