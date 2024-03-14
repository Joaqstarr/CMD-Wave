using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCommand : CommandBase
{
    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg = null)
    {
        overrideContext = null;
        sfx = null;
        return null;
    }
}
