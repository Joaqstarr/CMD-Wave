using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DropRoomCommand : CommandBase
{
    private bool _awaitingInput = false;
    [SerializeField]
    private CommandContext _awaitingInputContext;
    private string _inputCommand;
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {
        if (_awaitingInput)
        {
            arg = _inputCommand;
        }

        overrideContext = null;
        if (arg != string.Empty)
        {
            _awaitingInput = false;

            if (int.TryParse(arg, out int pos))
            {
                if (VesselRoomHandler.Instance.RemoveRoom(IntToPos(pos), out string droppedKey)){
                    if (droppedKey != string.Empty)
                    {

                        Item droppedItem = ItemPool.Instance.GetFreeItem(droppedKey);
                        if (droppedItem != null)
                        {
                            droppedItem.Spawn(droppedKey);

                            droppedItem.transform.position = transform.position;

                        }
                    }
                    return CommandLineManager.StringToArray("Room Removed: " + droppedKey.ToUpper());

                }
                else
                {
                    VesselRoomHandler.Instance.UpdateMap(false);
                    _awaitingInput = false;
                    return CommandLineManager.StringToArray("Invalid Room Position");
                }
            }
            else
            {
                int amountOfRooms = VesselRoomHandler.Instance.AmountOfRooms(arg);

                if (arg != " " && amountOfRooms > 0)
                {
                    if(amountOfRooms <= 1)
                    {
                        if (VesselRoomHandler.Instance.RemoveRoom(arg, out string droppedKey))
                        {
                            if (droppedKey != string.Empty)
                            {

                                Item droppedItem = ItemPool.Instance.GetFreeItem(droppedKey);
                                if (droppedItem != null)
                                {
                                    droppedItem.Spawn(droppedKey);

                                    droppedItem.transform.position = transform.position;

                                }
                            }
                            return CommandLineManager.StringToArray("Room Removed: " + droppedKey.ToUpper());

                        }
                    }
                    else
                    {
                        Map.Instance.MakeAllSelectable(arg);
                        _awaitingInput  = true;
                        overrideContext = _awaitingInputContext;

                        return CommandLineManager.StringToArray("More than 1 room of type attached. Please enter the coordinates for 1.");

                    }
                }


                VesselRoomHandler.Instance.UpdateMap(false);
                _awaitingInput = false;
                return CommandLineManager.StringToArray("Invalid Room Position 2" + arg + ", " + amountOfRooms);
            }
        }
        else
        {
            if (!_awaitingInput)
            {
                _awaitingInput=true;
                overrideContext = _awaitingInputContext;
                Map.Instance.MakeAllSelectable();
                return CommandLineManager.StringToArray("Enter Room Location:");

            }



        }
        VesselRoomHandler.Instance.UpdateMap(false);
        _awaitingInput = false;
        return CommandLineManager.StringToArray("Invalid Room Position 3");

    }
    public override bool CheckCommand(string commandName)
    {
        if (_awaitingInput)
        {
            if (int.TryParse(commandName, out int location))
            {
                Vector2Int position = new Vector2Int(Mathf.FloorToInt(location / 10f), location % 10);
                if (Map.Instance.GetSelectable().Contains(position))
                {
                    _inputCommand = location.ToString();

                    return true;
                }

            }
            VesselRoomHandler.Instance.UpdateMap(false);
            _inputCommand = "";
            _awaitingInput = false;
            return false;
        }
        else
        {
            return commandName.ToLower() == _commandName.ToLower();
        }
    }
    private Vector2Int IntToPos(int pos) {
        return new Vector2Int(Mathf.FloorToInt(pos/10f), pos%10);
    }
}
