using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPool : MonoBehaviour
{
    Room[] _rooms;
    public static RoomPool Instance { get; private set; }
    Dictionary<string, Room> _roomsDictionary;
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _rooms = GetComponentsInChildren<Room>();
        foreach (Room room in _rooms)
        {
            _roomsDictionary.Add(room.RoomTag.ToLower(), room);
        }
    }
    public Room GetRoom(string key)
    {
        key = key.ToLower();

        if (!_roomsDictionary.ContainsKey(key))
        {
            return null;
        }

        Room room = _roomsDictionary[key];

        return room;
    }
}
