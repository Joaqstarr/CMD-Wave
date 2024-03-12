using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;
    Map_Room[] _mapRooms;

    private Dictionary<Vector2Int, Map_Room> _locationMap = new Dictionary<Vector2Int, Map_Room>();
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _mapRooms = GetComponentsInChildren<Map_Room>();
        for(int i =0; i < _mapRooms.Length; i++)
        {
            _locationMap.Add(_mapRooms[i].PositionVector, _mapRooms[i]);
        }
    }
    public List<Vector2Int> GetSelectable()
    {
        List<Vector2Int> list = new List<Vector2Int>();
        for(int i = 0; i < _mapRooms.Length; i++)
        {
            if (_mapRooms[i].IsSelectable)
            {
                list.Add(_mapRooms[i].PositionVector);
            }
        }
        return list;
    }


    public void GenerateMap(Room[] roomList, bool showSelectable = false)
    {
        Debug.Log(_locationMap.Count);
        for (int i = 0; i < _mapRooms.Length; i++)
        {
            _mapRooms[i].Deactivate();

        }

        for(int i = 0; i < roomList.Length; i++)
        {
            if (_locationMap.ContainsKey(roomList[i].PositionVector))
            {
                Map_Room curRoom = _locationMap[roomList[i].PositionVector];
                curRoom.Activate(roomList[i].RoomTag);
                if (showSelectable)
                {
                    if (curRoom.RoomTag == "" || curRoom.RoomTag == " ")
                        curRoom.MakeSelectable();

                    if (curRoom.OnDown != null)
                        if (!curRoom.OnDown.Activated)
                        {
                            curRoom.OnDown.MakeSelectable();
                        }

                    if (curRoom.OnLeft != null)
                        if (!curRoom.OnLeft.Activated)
                        {

                            curRoom.OnLeft.MakeSelectable();
                        }

                    if (curRoom.OnRight != null)
                        if (!curRoom.OnRight.Activated)
                        {


                            curRoom.OnRight.MakeSelectable();
                        }

                    if (curRoom.OnUp != null)
                        if (!curRoom.OnUp.Activated)
                        {
                            curRoom.OnUp.MakeSelectable();
                        }


                }
            }
        }
    }


    public void MakeAllSelectable(string tagtoCheck = "")
    {   

        for (int i = 0; i < _mapRooms.Length; i++)
        {
            if (_mapRooms[i].Activated && _mapRooms[i].RoomTag != "")
            {
                if (tagtoCheck != "")
                {
                    if (_mapRooms[i].RoomTag.ToLower() == tagtoCheck.ToLower())
                    {
                        _mapRooms[i].MakeSelectable();

                    }
                }
                else
                    _mapRooms[i].MakeSelectable();

            }

        }
    }
}
