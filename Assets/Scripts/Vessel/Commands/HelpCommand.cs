using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCommand : CommandBase
{
    [SerializeField]private CommandLineManager _commandLineManager;
    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;

        overrideContext = _commandLineManager.CommandOveride;
        string[] Output;
        CommandBase[] possibleCommands;

        if (arg.Length > 0)
        {
            possibleCommands = VesselRoomHandler.Instance.GetCommandsFromKey(arg);
            if(possibleCommands == null)
            {
                return CommandLineManager.StringToArray("Room does not exist.");
            }
            else
            {

                Output = new string[Mathf.Max(2, possibleCommands.Length + 1)];
                Output[0] = "Possible Commands for Room " + arg.ToUpper();
                if(possibleCommands.Length == 0)
                {
                    Output[1] = "NONE";
                }else
                for (int i = 1; i < Output.Length; i++)
                {
                    Output[i] = possibleCommands[i-1].GetHelpLine();
                }

                return Output;
            }



        }
        possibleCommands = _commandLineManager.GetPossibleCommands();

        Output = new string[possibleCommands.Length + 1];
        Output[0] = "Use \"HELP __\" to receive information on a specific Room.\nPossible Commands:";
        for (int i = 1; i < Output.Length; i++)
        {
            Output[i] = possibleCommands[i-1].GetHelpLine();
        }
        return Output;
    }
}
