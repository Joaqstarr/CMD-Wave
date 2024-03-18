using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipCommand : CommandBase
{
    private string _abilityTag;
    private string _abilityName;

    private void Awake()
    {
        _abilityTag = GetComponent<Room>().RoomTag;
        _abilityName = GetComponent<Room>().RoomName;
    }
    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx, string arg = null)
    {
        sfx = _soundWhenExecuted;

        overrideContext = null;

        if(AbilityManager.Main._activeAbility != null)
        if (AbilityManager.Main._activeAbility._data.commandShortcut.ToLower().Equals(_abilityTag.ToLower()))
            return CommandLineManager.StringToArray(_abilityName + " is already equipped");

        AbilityManager.Main.Equip(_abilityTag);
        return CommandLineManager.StringToArray("Equipped " + _abilityName);
    }

    public override bool CheckCommand(string commandName)
    {
        if (commandName.ToLower() == _abilityTag.ToLower() || commandName.ToLower() == _abilityName.ToLower())
            return true;

        return _commandName.ToLower() == commandName.ToLower();
    }
}
