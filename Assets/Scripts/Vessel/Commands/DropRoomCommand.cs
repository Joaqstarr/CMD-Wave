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
                            droppedItem.gameObject.SetActive(true);

                            droppedItem.transform.position = transform.position;

                        }
                    }
                    return CommandLineManager.StringToArray("Room Removed: " + droppedKey.ToUpper());

                }
                else
                {
                    return CommandLineManager.StringToArray("Invalid Room Position");
                }
            }
            {
                return CommandLineManager.StringToArray("Invalid Room Position");
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
        return CommandLineManager.StringToArray("Invalid Room Position");

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
