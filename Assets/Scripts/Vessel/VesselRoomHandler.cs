using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselRoomHandler : MonoBehaviour
{
    [SerializeField]
    Room _commandRoom;
    Room[] _rooms;

    Dictionary<Vector2Int, Room> PlacedRooms = new Dictionary<Vector2Int, Room>();
    public static VesselRoomHandler Instance {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        if(_commandRoom == null)
        {
            _commandRoom = GetComponentInChildren<Room>();
        }

        AddRoom(_commandRoom, _commandRoom.PositionVector);
    }

    public void AddRoom(Room room, Vector2Int position)
    {
        room.transform.parent = transform;
        room.Position = (position.x*10)+ position.y;
        Debug.Log(position.ToString());
        PlacedRooms.Add(position, room);

        //make links

        Vector2Int left = new Vector2Int(position.x, position.y-1);
        Vector2Int right = new Vector2Int(position.x, position.y+1);
        Vector2Int up = new Vector2Int(position.x -10, position.y);
        Vector2Int down = new Vector2Int(position.x+10, position.y);

        if (PlacedRooms.ContainsKey(left))
        {
            room.Left = PlacedRooms[left];
            PlacedRooms[left].Right = room;
        }
        if (PlacedRooms.ContainsKey(right))
        {
            room.Right = PlacedRooms[right];
            PlacedRooms[right].Left = room;

        }
        if (PlacedRooms.ContainsKey(down))
        {
            room.Down = PlacedRooms[down];
        }
        if(PlacedRooms.ContainsKey(up))
        {
            PlacedRooms[up].Down = room;
        }
        UpdateMap(false);
    }

    public bool RemoveRoom(Vector2Int position)
    {
        if(PlacedRooms.ContainsKey(position))
        {
            PlacedRooms[position].transform.parent = RoomPool.Instance.transform;
            PlacedRooms.Remove(position);
            UpdateRooms();

            return true;
        }

        return false;
    }
    private void UpdateRooms()
    {
        _rooms = GetComponentsInChildren<Room>();

    }

    public void UpdateMap(bool showSelectable){
        UpdateRooms();
        Map.Instance.GenerateMap(_rooms, showSelectable);

    }
}
