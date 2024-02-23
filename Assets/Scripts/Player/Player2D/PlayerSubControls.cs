using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSubControls : PlayerControls
{

    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _aimInput = Vector2.zero;
    // control aim?
    private PlayerInput _input;

    void Start()
    {
        _input = GetComponent<PlayerInput>();
    }

    public override bool OnSubMove(InputValue Value)
    {
        //if (base.OnSubMove(Value)) return false;

        // input code
        _moveInput = Value.Get<Vector2>();

        return true;
    }

    public override bool OnMouseAim(InputValue Value)
    {
        if (base.OnMouseAim(Value)) return false;

        // input code
        _aimInput = Value.Get<Vector2>();

        return base.OnMouseAim(Value);
    }

    public override bool OnAim(InputValue Value)
    {
        return base.OnAim(Value);
    }

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    public Vector2 AimInput
    {
        get { return _aimInput; }
    }
}
