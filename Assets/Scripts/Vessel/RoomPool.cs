using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomPool : MonoBehaviour
{
    Room[] _rooms;
    public static RoomPool Instance { get; private set; }
    Dictionary<string, Room> _roomsDictionary = new Dictionary<string, Room>();

    [SerializeField]
    private Room _hallwayPrefab;
    [SerializeField]
    private int _hallwayCount = 15;
    private List<Room> _hallways = new List<Room>();
    private void Awake()
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
            if(!_roomsDictionary.ContainsKey(room.RoomTag.ToLower()))
                _roomsDictionary.Add(room.RoomTag.ToLower(), room);
        }

        for(int i = 0; i < _hallwayCount; i++)
        {
            Room spawnedHall = Instantiate(_hallwayPrefab, Vector3.zero, _hallwayPrefab.transform.rotation,transform);
            spawnedHall.transform.localPosition = Vector3.zero;
            _hallways.Add (spawnedHall);
        }
    }
    public Room GetRoom(string key)
    {
        key = key.ToLower();

        if(key == " " || key == "")
        {
            return GetHall();
        }
        if (!_roomsDictionary.ContainsKey(key))
        {
            return null;
        }
        for(int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].RoomTag.ToLower() == key && _rooms[i].transform.parent == transform)
                return _rooms[i];
        }
        //Room room = _roomsDictionary[key];

        return null;
    }

    public Room GetHall()
    {
        for(int i =0 ; i < _hallways.Count; i++)
        {
            if (_hallways[i].transform.parent == transform)
            {
                return _hallways[i];
            }
        }

        Room spawnedHall = Instantiate(_hallwayPrefab, Vector3.zero, _hallwayPrefab.transform.rotation, transform);
        _hallways.Add(spawnedHall);
        return spawnedHall;
    }
}
