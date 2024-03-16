using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOnCommand : CommandBase
{
    [SerializeField]
    private CommandContext _dropRoomContext;
    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg = null)
    {
        overrideContext = _dropRoomContext;
        sfx = null;
        CommandLineManager.Instance.PowerOn();

        string[] output = new string[2];
        output[0] = "Powering On...";
        output[1] = "Welcome Captain. \n\n To Begin use the \"DROP\" command to disconnect from the dock. You can use the \"HELP\" command at any time to find out what you can input.";
        return output;
    }
}
