using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCommand : CommandBase
{
    [SerializeField]private CommandLineManager _commandLineManager;
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        overrideContext = _commandLineManager.CommandOveride;

        CommandBase[] possibleCommands = _commandLineManager.GetPossibleCommands();

        string[] Output = new string[possibleCommands.Length];
        for (int i = 0; i < possibleCommands.Length; i++)
        {
            Output[i] = possibleCommands[i].GetHelpLine();
        }
        return Output;
    }
}
