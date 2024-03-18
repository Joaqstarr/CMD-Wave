using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext,out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;
        
        overrideContext = null;
        if (FinalBossManager.BossStarted)
        {
            return CommandLineManager.StringToArray("...save failed.");

        }

        if (PlayerSubHealth.Instance != null)
            if (PlayerSubHealth.Instance.Health < 30)
            {
                return CommandLineManager.StringToArray("Hull Integrity too low");
            }
        SaveManager.Instance.Save();
        return CommandLineManager.StringToArray("Data Succesfully Saved.");
    }


}
