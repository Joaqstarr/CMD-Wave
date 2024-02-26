using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSubControls : PlayerControls
{

    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _aimInput = Vector2.zero;
    // control aim?



    #region Control Events
    public delegate void CommandLineDel();
    public static CommandLineDel openCommandLine;
    #endregion


    public override bool OnSubMove(InputValue Value)
    {
        if (!base.OnSubMove(Value)) return false;

        // input code
        _moveInput = Value.Get<Vector2>();

        return true;
    }

    public override bool OnMouseAim(InputValue Value)
    {
        if (!base.OnMouseAim(Value)) return false;

        // input code
        _aimInput = Value.Get<Vector2>();

        return true;
    }

    public override bool OnAim(InputValue Value)
    {
        if (!base.OnAim(Value)) return false;

        return true;
    }

    public override bool OnCommandLine(InputValue Value)
    {
        if (!base.OnCommandLine(Value)) return false;

        if(openCommandLine != null)
        {
            openCommandLine();
        }

        return true;
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
