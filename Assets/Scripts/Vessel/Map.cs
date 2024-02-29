using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;
    Map_Room[] _mapRooms;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _mapRooms = GetComponentsInChildren<Map_Room>();
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
        Dictionary<int, string> map = new Dictionary<int, string>();
        for (int i = 0; i < roomList.Length; i++)
        {
            map.Add(roomList[i].Position, roomList[i].RoomTag);
        }
        for (int i = 0; i < _mapRooms.Length; i++)
        {
            _mapRooms[i].Deactivate();

        }
        for (int i = 0;i < _mapRooms.Length; i++)
        {
            if (map.ContainsKey(_mapRooms[i].Position))
            {
                _mapRooms[i].Activate(map[_mapRooms[i].Position]);
                
                if(showSelectable)
                {
                    if(_mapRooms[i].OnDown != null)
                        if (!_mapRooms[i].OnDown.Activated)
                        {
                            _mapRooms[i].OnDown.MakeSelectable();
                        }

                    if (_mapRooms[i].OnLeft != null)
                        if (!_mapRooms[i].OnLeft.Activated)
                        {

                            _mapRooms[i].OnLeft.MakeSelectable();
                        }

                    if (_mapRooms[i].OnRight != null)
                        if (!_mapRooms[i].OnRight.Activated)
                        {


                            _mapRooms[i].OnRight.MakeSelectable();
                        }

                    if (_mapRooms[i].OnUp != null)
                        if (!_mapRooms[i].OnUp.Activated)
                        {


                            _mapRooms[i].OnUp.MakeSelectable();
                        }


                }
                
            }

        }
    }
}
