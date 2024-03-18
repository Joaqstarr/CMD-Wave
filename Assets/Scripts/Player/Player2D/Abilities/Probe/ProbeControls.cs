using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProbeControls : PlayerControls
{
    public static ProbeControls Instance;
    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _aimInput = Vector2.zero;
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
    public override bool OnMouseAim(InputValue Value)
    {
        if (!base.OnMouseAim(Value)) return false;

        // mouse transformation to 2d cam space
        if (_plane != null)
            _aimInput = TransformMouseInput(Value.Get<Vector2>());
        else
            _aimInput = Value.Get<Vector2>();







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
    }
    public override void OnUnPosessed()
    {
        base.OnUnPosessed();

        _moveInput = Vector2.zero;
        _powerPressed = false;
    }

    [SerializeField] RectTransform _plane;
    private Vector2 TransformMouseInput(Vector2 input)
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_plane, input, Camera.main, out localpoint);
        localpoint.x += _plane.rect.width / 2;
        localpoint.y += _plane.rect.height / 2;


        return localpoint;
    }

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    public bool PowerPressed
    { 
        get { return _powerPressed; }
    }

    public Vector2 AimInput
    {
        get { return _aimInput; }
        set { _aimInput = value; }
    }


}
