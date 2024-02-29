using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselRoomHandler : MonoBehaviour
{
    [SerializeField]
    Room _commandRoom;

    Dictionary<Vector2Int, Room> PlacedRooms;
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
        PlacedRooms.Add(position, room);

        //make links

        Vector2Int left = new Vector2Int(position.x -1, position.y);
        Vector2Int right = new Vector2Int(position.x +1, position.y);
        Vector2Int up = new Vector2Int(position.x , position.y+10);
        Vector2Int down = new Vector2Int(position.x, position.y-10);

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

    }

    public bool RemoveRoom(Vector2Int position)
    {
        if(PlacedRooms.ContainsKey(position))
        {
            PlacedRooms[position].transform.parent = RoomPool.Instance.transform;
            PlacedRooms.Remove(position);

            return true;
        }

        return false;
    }
}
