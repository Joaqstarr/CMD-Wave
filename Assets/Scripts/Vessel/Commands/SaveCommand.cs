using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        overrideContext = null;
        SaveManager.Instance.Save();
        return CommandLineManager.StringToArray("Data Succesfully Saved.");
    }


}
