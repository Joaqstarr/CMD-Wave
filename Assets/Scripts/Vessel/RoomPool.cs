using System.Collections;
using System.Collections.Generic;
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

        for(int i = 0; i < _hallwayCount; i++)
        {
            Room spawnedHall = Instantiate(_hallwayPrefab, Vector3.zero, _hallwayPrefab.transform.rotation,transform);
            _hallways.Add (spawnedHall);
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

    public Room GetHall()
    {
        for(int i =0 ; i < _hallwayCount;i++)
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
