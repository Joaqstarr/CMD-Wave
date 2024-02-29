using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupCommand : CommandBase
{
    private List<Item> _itemsInRange = new List<Item>();
    private bool _isAwaitingItemName = false;
    private string _itemToPickup = "";
    [SerializeField]
    private CommandContext _waitForNameContext;
    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {

        overrideContext = null;
        if(_itemsInRange.Count <= 0)
        {
            return CommandLineManager.StringToArray("No Items Nearby");
        }
        if (arg != string.Empty)
        {
            int itemPickupIndex = CheckListForArg(arg);
            if (itemPickupIndex >= 0)
            {
                //pickup item

                return CommandLineManager.StringToArray("Picking up item " + _itemsInRange[itemPickupIndex].RoomCode);

            }

        }
        else
        {
            if (!_isAwaitingItemName)
            {
                overrideContext = _waitForNameContext;
                _isAwaitingItemName = true;
                _itemToPickup = "";
                return CommandLineManager.StringToArray("Please Enter Item Name:");
            }
            else
            {
                int itemPickupIndex = CheckListForArg(_itemToPickup);
                if (itemPickupIndex >= 0)
                {
                    _isAwaitingItemName = false;
                    //pickup item


                    return CommandLineManager.StringToArray("Picking up item " + _itemsInRange[itemPickupIndex].RoomCode);

                }
                else
                {
                    return CommandLineManager.StringToArray("Invalid Item Name");
                }
            }
        }
        return CommandLineManager.StringToArray("Invalid Item Name");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _itemsInRange.Add(other.GetComponent<Item>());
        }    
    }
    private int CheckListForArg(string arg)
    {
        for(int i = 0;  i < _itemsInRange.Count; i++)
        {
            if (_itemsInRange[i].RoomCode.ToLower() == arg.ToLower())
                return i;
        }
        return -1;
    }
    public override bool CheckCommand(string commandName)
    {
        if (_isAwaitingItemName)
        {
            _itemToPickup = commandName.ToLower();
            return true;
        }
        _itemToPickup = null;

        return _commandName.ToLower() == commandName.ToLower();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _itemsInRange.Remove(other.GetComponent<Item>());
        }
    }
}
