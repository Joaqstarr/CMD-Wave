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

    
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        overrideContext = null;
        if(_position.x < 0)
        {
            return CommandLineManager.StringToArray("Invalid Room!");
        }
        VesselRoomHandler.Instance.AddRoom(_pickupCommand.RoomAdding, _position);
        
        return CommandLineManager.StringToArray("Adding Room...");

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
            else
            {
                _position = -Vector2Int.one;
                return false;
            }

        }
        _position = -Vector2Int.one;
        return false;
    }
}
