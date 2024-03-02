using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

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
        // apply friction
        ApplyFriction();

        // draw view cone
        player._viewCone.DrawViewCone(player.SubControls.AimInput);
    }

    private void ApplyFriction()
    {
        Vector2 input = Vector2.zero;
        // move force
        Vector2 target = new Vector2(input.x * _subData.moveSpeed, input.y * _subData.moveSpeed);

        float difX = target.x - _rb.velocity.x;
        float difY = target.y - _rb.velocity.y;

        float accelVal = (input.magnitude < _rb.velocity.magnitude) ? _subData.acceleration : _subData.decceleration;

        float forceX = Mathf.Pow(Mathf.Abs(difX) * accelVal, _subData.velocityModifier) * Mathf.Sign(difX);
        float forceY = Mathf.Pow(Mathf.Abs(difY) * accelVal, _subData.velocityModifier) * Mathf.Sign(difY);
        Vector2 netForce = new Vector2(forceX, forceY);

        _rb.AddForce(netForce);

        // friction
        if (_subControls.MoveInput.x > 0.01)
        {
            float frictionX = Mathf.Min(Mathf.Abs(_subData.frictionModifier), Mathf.Abs(_rb.velocity.x));

            frictionX *= Mathf.Sign(_rb.velocity.x);

            float frictionY = Mathf.Min(Mathf.Abs(_subData.frictionModifier), Mathf.Abs(_rb.velocity.y));

            frictionY *= Mathf.Sign(_rb.velocity.y);

            _rb.AddForce(new Vector2(-frictionX, -frictionY), ForceMode.Impulse);
        }
    }
}
