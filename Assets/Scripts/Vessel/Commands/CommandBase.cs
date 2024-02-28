using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommandBase : MonoBehaviour
{
    [SerializeField]
    protected string _commandName;
    [SerializeField]
    protected bool _shouldClear;
    public abstract string[] Execute(string arg =null);

    public bool CheckCommand(string commandName)
    {
        return _commandName == commandName;
    }

    public bool ShouldClear{

        get { return _shouldClear; }

        }
}
