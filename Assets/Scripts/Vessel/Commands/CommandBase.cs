using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandBase : MonoBehaviour
{
    [SerializeField]
    protected string _commandName;
    protected bool _shouldClear;
    public abstract string Execute();

    public bool CheckCommand(string commandName)
    {
        return _commandName == commandName;
    }

    public bool ShouldClear{

        get { return _shouldClear; }

        }
}
