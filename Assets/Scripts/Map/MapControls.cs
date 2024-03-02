using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapControls : PlayerControls
{
    private Vector2 _moveInput;
    public override bool OnMapMove(InputValue Value)
    {
        if(!base.OnMapMove(Value))return false;

        _moveInput = Value.Get<Vector2>();
        return true;
    }

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

}
