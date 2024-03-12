using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        overrideContext = null;
        SaveManager.Instance.Load();
        return CommandLineManager.StringToArray("Data Succesfully Loaded");
    }
}
