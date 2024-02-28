using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    Map_Room[] _mapRooms;
    // Start is called before the first frame update
    void Start()
    {
        _mapRooms = GetComponentsInChildren<Map_Room>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateMap(Room[] roomList)
    {
        Dictionary<int, string> map = new Dictionary<int, string>();
        for (int i = 0; i < roomList.Length; i++)
        {
            map.Add(roomList[i].Position, roomList[i].RoomTag);
        }

        for (int i = 0;i < _mapRooms.Length; i++)
        {
            if (map.ContainsKey(_mapRooms[i].Position))
            {
                _mapRooms[i].Activate(map[_mapRooms[i].Position]);
            }
            else
            {
                _mapRooms[i].Deactivate();
            }
        }
    }
}
