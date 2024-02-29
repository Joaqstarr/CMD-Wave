using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupCommand : CommandBase
{
    private List<Item> _itemsInRange = new List<Item>();
    private bool _isAwaitingItemName = false;
    private string _itemToPickup = "";

    private Room _roomToAdd;
    
    [SerializeField]
    private CommandContext _waitForNameContext;
    [SerializeField]
    private CommandContext _waitForLocationContext;
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
                _roomToAdd = RoomPool.Instance.GetRoom(_itemsInRange[itemPickupIndex].RoomCode);
                overrideContext = _waitForLocationContext;
                VesselRoomHandler.Instance.UpdateMap(true);
                return GetPickUpMessage(_itemsInRange[itemPickupIndex].RoomCode);

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
                    _roomToAdd = RoomPool.Instance.GetRoom(_itemsInRange[itemPickupIndex].RoomCode);

                    overrideContext = _waitForLocationContext;
                    VesselRoomHandler.Instance.UpdateMap(true);

                    return GetPickUpMessage(_itemsInRange[itemPickupIndex].RoomCode);

                }
                else
                {
                    return CommandLineManager.StringToArray("Invalid Item Name");
                }
            }
        }
        return CommandLineManager.StringToArray("Invalid Item Name");

    }
    private string[] GetPickUpMessage(string code)
    {
         string[] message = new string[2];
         message[0] = "Picking up item " + code;
         message[1] = "Enter room coordinate: ";

        return message;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            _itemsInRange.Add(other.GetComponent<Item>());
        }    
    }
    public int CheckListForArg(string arg)
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
    public void RemoveItem(Item item)
    {
        if(_itemsInRange.Contains(item))
            _itemsInRange.Remove(item);
    }
    public Room RoomAdding
    {
        get { return _roomToAdd; }
    }
    public Item GetItem(string key)
    {
        for (int i = 0; i < _itemsInRange.Count; i++)
        {
            if (_itemsInRange[i].RoomCode.ToLower() == key.ToLower())
                return _itemsInRange[i];
        }
        return null;
    }
}
