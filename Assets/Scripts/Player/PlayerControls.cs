using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerControls _possesedInput;
    [SerializeField]
    private string _controlMap;
    private PlayerInput _input;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        
    }
    public virtual bool OnFPMove(InputValue Value)
    {
        if(_possesedInput != null)
        {
            _possesedInput.OnFPMove(Value);

            return false;
        }
        return true;
    }

    public virtual bool OnLook(InputValue Value)
    {
        if (_possesedInput != null)
        {
            _possesedInput.OnLook(Value);
            return false;
        }

        return true;

    }
    public virtual bool OnSelect(InputValue Value)
    {
        if (_possesedInput != null)
        { 
            _possesedInput.OnSelect(Value);
            return false;
        }

        return true;

    }
    public virtual bool OnSubMove(InputValue Value)
    {
        if (_possesedInput != null)
        {
            _possesedInput.OnSubMove(Value);
            return false;
        }

        return true;

    }

    public virtual bool OnAim(InputValue Value)
    {
        if (_possesedInput != null)
        {
            _possesedInput.OnAim(Value);
            return false;
        }

        return true;

    }

    public virtual bool OnMouseAim(InputValue Value)
    {
        if (_possesedInput != null)
        {
            _possesedInput.OnMouseAim(Value);
            return false;
        }

        return true;

    }
    
    public virtual bool OnCommandLine(InputValue Value)
    {
        if(_possesedInput != null)
        {
            _possesedInput.OnCommandLine(Value);
            return false;
        }

        return true;
    }
    public void Possess(PlayerControls inputToPosess)
    {
        _possesedInput = inputToPosess;

        if (_input != null)
        {
            _input.SwitchCurrentActionMap(ControlMap);
            _possesedInput.Input = Input;
        }

    }

    public void UnPossess()
    {
        _possesedInput = null;
    }

    public string ControlMap
    {
        get { 
            if(_possesedInput == null)
            {
                return _controlMap;
            }
            else
            {
                return _possesedInput.ControlMap;
            }
        } 
        set { 
            _controlMap = value;
        }
    }

    public PlayerInput Input { 
        get { return _input; } 
        set { 
            if(_possesedInput != null)
            {
                _possesedInput.Input = Input;
            }
            _input = value; 
        }
    }
}
