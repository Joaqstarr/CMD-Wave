using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProbeControls : PlayerControls
{
    private Vector2 _moveInput;
    public override bool OnProbeMove(InputValue Value)
    {
        if (!base.OnProbeMove(Value)) return false;

        _moveInput = Value.Get<Vector2>();
        return true;
    }

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }
}
