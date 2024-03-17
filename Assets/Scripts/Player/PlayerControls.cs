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
    public static float MouseSens = 1;
    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        
    }
    public virtual bool OnFPMove(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {

            _possesedInput.OnFPMove(Value);

            return false;
        }
        return true;
    }

    public virtual bool OnLook(InputValue Value)
    {

        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null )
        {
            _possesedInput.OnLook(Value);
            return false;
        }

        return true;

    }
    public virtual bool OnSelect(InputValue Value)
    {

        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null )
        {
            _possesedInput.OnSelect(Value);
            return false;
        }

        return true;

    }
    public virtual bool OnSubMove(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null )
        {

            _possesedInput.OnSubMove(Value);
            return false;
        }

        return true;

    }

    public virtual bool OnAim(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null )
        {
            _possesedInput.OnAim(Value);
            return false;
        }

        return true;

    }

    public virtual bool OnMouseAim(InputValue Value)
    {
        if(CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null )
        {


            _possesedInput.OnMouseAim(Value);
            return false;
        }

        return true;

    }
    
    public virtual bool OnCommandLine(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnCommandLine(Value);
            return false;
        }

        return true;
    }

    public virtual bool OnEquippedPower(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnEquippedPower(Value);
            return false;
        }

        return true;
    }

    public virtual bool OnMapMove(InputValue Value)
    {

        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnMapMove(Value);
            return false;
        }

        return true;
    }

    public virtual bool OnProbeMove(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnProbeMove(Value);
            return false;
        }

        return true;
    }


    public virtual bool OnExit(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnExit(Value);
            return false;
        }

        return true;
    }
    public virtual bool OnPause(InputValue Value)
    {
        if (CommandLineManager.Instance != null)
        {
            if (CommandLineManager.Instance.IsTyping)
            {
                return false;
            }
        }
        if (_possesedInput != null)
        {
            _possesedInput.OnPause(Value);
            return false;
        }

        return true;
    }
    public void Possess(PlayerControls inputToPosess, bool overrideChain = false)
    {
        if (!overrideChain)
        {
            if(_possesedInput != null)
            {
                _possesedInput.Possess(inputToPosess, true);
            }
            else
            {
                _possesedInput = inputToPosess;
            }
        }
        else
        {
            _possesedInput = inputToPosess;

        }

        if (_input != null)
        {
            _input.SwitchCurrentActionMap(ControlMap);
            _possesedInput.Input = Input;
        }

        inputToPosess.OnPosessed();

    }

    public void UnPossess()
    {
        if (_possesedInput == null) return;

        _possesedInput.OnUnPosessed();
        _possesedInput = null;
        if (_input != null)
        {
            _input.SwitchCurrentActionMap(ControlMap);
        }
    }
    public void UnPossess(PlayerControls controlToUnposess)
    {
        if (_possesedInput == null) return;

        if (_possesedInput == controlToUnposess || _possesedInput == null)
        {
            UnPossess();

        }
        else
        {
            _possesedInput.UnPossess(controlToUnposess);

        }
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

    public virtual void OnPosessed()
    {
        Debug.Log(this + " posessed");

    }
    public virtual void OnUnPosessed()
    {
        Debug.Log(this + " unpossessed");
    }

    public bool IsPosessing
    {
        get { return _possesedInput != null; }
    }

}
