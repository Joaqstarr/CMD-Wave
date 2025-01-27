using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSubControls : PlayerControls, IDataPersistance
{
    public static PlayerSubControls Instance;
    private Vector2 _moveInput = Vector2.zero;
    private Vector2 _aimInput = Vector2.zero;
    private bool _powerPressed;
    // control aim?

    [SerializeField]
    private CinemachineVirtualCamera _screenCamera;
    private Rigidbody _rigidbody;


    #region Control Events
    public delegate void CommandLineDel();
    public static CommandLineDel openCommandLine;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _rigidbody = GetComponent<Rigidbody>();
    }
    public override bool OnSubMove(InputValue Value)
    {
        if (!base.OnSubMove(Value)) {
            _moveInput = Vector2.zero;
            return false;
        } 

        // input code
        _moveInput = Value.Get<Vector2>();

        return true;
    }

    public override bool OnMouseAim(InputValue Value)
    {
        if (!base.OnMouseAim(Value)) return false;

        // mouse transformation to 2d cam space
        if(_plane != null)
            _aimInput = TransformMouseInput(Value.Get<Vector2>());
        else
            _aimInput = Value.Get<Vector2>();







        return true;
    }
    public override bool OnExit(InputValue Value)
    {
        if (CommandLineManager.Instance.IsTyping) return false;
        if(FirstPersonPlayerControls.Instance != null)
            FirstPersonPlayerControls.Instance.UnPossess();

        if (CommandLineManager.Instance != null)
            CommandLineManager.Instance.EndCommand();

        return true;
    }
    public override bool OnAim(InputValue Value)
    {
        if (!base.OnAim(Value)) return false;

        return true;
    }

    public override bool OnCommandLine(InputValue Value)
    {
        //if (!base.OnCommandLine(Value)) return false;
        if (CommandLineManager.Instance.IsTyping) return false;
        if(openCommandLine != null)
        {
            openCommandLine();
        }

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

    public Vector2 MoveInput
    {
        get { return _moveInput; }
    }

    public Vector2 AimInput
    {
        get { return _aimInput; }
        set { _aimInput = value; }
    }
    public bool PowerPressed
    {
        get { return _powerPressed; }
        set { _powerPressed = value; }
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

    public override void OnPosessed()
    {
        base.OnPosessed();

        if(_screenCamera != null)
            _screenCamera.Priority = 11;
    }
    public override void OnUnPosessed()
    {
        base.OnUnPosessed();

        _moveInput = Vector2.zero;
        _powerPressed = false;
        

        if (_screenCamera != null)
            _screenCamera.Priority = 0;
    }

    public void SaveData(ref SaveData data)
    {
        float[] position = { transform.position.x, transform.position.y };
        data._subPosition = position;
    }

    public void LoadData(SaveData data)
    {

        _rigidbody.MovePosition(new Vector2(data._subPosition[0], data._subPosition[1]));
    }
}
