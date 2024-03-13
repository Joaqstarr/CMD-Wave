using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PickRoomLocationCommand : CommandBase
{
    private Vector2Int _position;
    [SerializeField]
    private ItemPickupCommand _pickupCommand;

    public override string[] Execute(out CommandContext overrideContext, out AudioClip sfx,string arg = null)
    {
        sfx = _soundWhenExecuted;

        overrideContext = null;
        if(_position.x < 0)
        {
            return CommandLineManager.StringToArray("Invalid Room!");
        }
        Item item = _pickupCommand.GetItem(_pickupCommand.RoomAdding.RoomTag);
        if(item == null)
        {
            VesselRoomHandler.Instance.UpdateMap(false);
            return CommandLineManager.StringToArray("Item No Longer In Range.");
        }

        VesselRoomHandler.Instance.AddRoom(_pickupCommand.RoomAdding, _position);
        item.Collect();
        _pickupCommand.RemoveItem(item);
        return CommandLineManager.StringToArray("Adding Room... Room Added. Enjoy!");

    }

    public override bool CheckCommand(string commandName)
    {
        Debug.Log("checking, " + commandName);
        if(int.TryParse(commandName, out int location))
        {
            Vector2Int position = new Vector2Int(Mathf.FloorToInt(location / 10f), location % 10);
            if (Map.Instance.GetSelectable().Contains(position))
            {
                _position = position;

                return true;
            }

        }
        VesselRoomHandler.Instance.UpdateMap(false);
        _position = -Vector2Int.one;
        return false;
    }

}
