using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext,out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;
        overrideContext = null;
        SaveManager.Instance.Load();
        return CommandLineManager.StringToArray("Data Succesfully Loaded");
    }
}
