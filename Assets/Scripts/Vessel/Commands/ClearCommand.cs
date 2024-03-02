using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCommand : CommandBase
{
    [SerializeField]
    private CommandLineManager _commandLineManager;
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        overrideContext = _commandLineManager.CommandOveride;



        return CommandLineManager.StringToArray("");
    }
}
