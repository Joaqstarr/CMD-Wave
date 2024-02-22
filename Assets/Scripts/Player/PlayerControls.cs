using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerControls _possesedInput;

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

    public void Possess(PlayerControls inputToPosess)
    {
        _possesedInput = inputToPosess;
    }

    public void UnPossess()
    {
        _possesedInput = null;
    }
}
