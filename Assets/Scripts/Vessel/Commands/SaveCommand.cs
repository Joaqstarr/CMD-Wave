using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext,out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;

        overrideContext = null;
        SaveManager.Instance.Save();
        return CommandLineManager.StringToArray("Data Succesfully Saved.");
    }


}
