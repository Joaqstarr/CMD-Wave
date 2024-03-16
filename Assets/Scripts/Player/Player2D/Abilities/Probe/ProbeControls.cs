using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProbeControls : PlayerControls
{
    public static ProbeControls Instance;
    private Vector2 _moveInput;
    private bool _powerPressed;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    public override bool OnProbeMove(InputValue Value)
    {
        if (!base.OnProbeMove(Value)) return false;

        _moveInput = Value.Get<Vector2>();
        Debug.Log(_moveInput);
        return true;
    }

    public override bool OnEquippedPower(InputValue Value)
    {
        if (!base.OnEquippedPower(Value))
        {
            _powerPressed = false;
            return false;
        }

        _powerPressed = Value.isPressed;

        return true;
    }

    public override void OnPosessed()
    {
        base.OnPosessed();

        PlayerSubControls.Instance.AimInput *= SubViewCone.subAimVector * 1000;
    }
    public override void OnUnPosessed()
    {
        base.OnUnPosessed();

        _moveInput = Vector2.zero;
        _powerPressed = false;
    }

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    public bool PowerPressed
    { 
        get { return _powerPressed; }
    }
}
