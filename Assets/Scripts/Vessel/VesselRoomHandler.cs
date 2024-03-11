using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselRoomHandler : MonoBehaviour
{
    [SerializeField]
    Room _commandRoom;
    Room[] _rooms;
    [SerializeField]
    private float _roomDistance = 7;
    [SerializeField]
    private Vector3 _startRoomArrayPosition;

    Dictionary<Vector2Int, Room> PlacedRooms = new Dictionary<Vector2Int, Room>();
    public static VesselRoomHandler Instance {  get; private set; }
    public CommandContext RoomsContext { get; private set; } = new CommandContext();

    [Header("Debug")]
    [SerializeField]
    private Vector2Int _roomArraySize;
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

        CommandLineManager.Instance.AddContext(RoomsContext);
    }

    public void AddRoom(Room room, Vector2Int position)
    {
        room.transform.parent = transform;
        room.Position = (position.x*10)+ position.y;

        if(!room.IsStatic) 
            room.transform.position = _startRoomArrayPosition + new Vector3(position.y* _roomDistance, 0,position.x*-_roomDistance);
        
        Debug.Log(position.ToString());
        PlacedRooms.Add(position, room);

        for(int i = 0;i<room.AssociatedCommands.Length; i++)
        {
            RoomsContext.AddCommand(room.AssociatedCommands[i]);
        }


        //make links
        Vector2Int left = new Vector2Int(position.x, position.y-1);
        Vector2Int right = new Vector2Int(position.x, position.y+1);
        Vector2Int up = new Vector2Int(position.x -1, position.y);
        Vector2Int down = new Vector2Int(position.x+1, position.y);

        if (PlacedRooms.ContainsKey(left))
        {
            room.Left = PlacedRooms[left];
            PlacedRooms[left].Right = room;
            PlacedRooms[left].UpdateDoors();

        }
        if (PlacedRooms.ContainsKey(right))
        {
            room.Right = PlacedRooms[right];
            PlacedRooms[right].Left = room;
            PlacedRooms[right].UpdateDoors();


        }
        if (PlacedRooms.ContainsKey(down))
        {
            room.Down = PlacedRooms[down];
            PlacedRooms[down].Up = room;
            PlacedRooms[down].UpdateDoors();

        }
        if (PlacedRooms.ContainsKey(up))
        {
            room.Up = PlacedRooms[up];
            PlacedRooms[up].Down = room;
            PlacedRooms[up].UpdateDoors();
        }

        room.UpdateDoors();
        UpdateMap(false);
    }

    public bool RemoveRoom(Vector2Int position, out string keyRemoved)
    {
        keyRemoved = "";
        if(PlacedRooms.ContainsKey(position))
        {
            if (PlacedRooms[position].RoomTag == " ") return false;

            Room roomRemoving = PlacedRooms[position];
            for (int i = 0; i < roomRemoving.AssociatedCommands.Length; i++)
            {
                RoomsContext.RemoveCommand(roomRemoving.AssociatedCommands[i]);
            }

            roomRemoving.transform.parent = RoomPool.Instance.transform;
            if (!roomRemoving.IsStatic)
            {
                roomRemoving.transform.localPosition = Vector3.zero;
            }




            keyRemoved = roomRemoving.RoomTag;

            PlacedRooms.Remove(position);

            if (roomRemoving.RoomConnectedCount > 1)
            {
                AddRoom(RoomPool.Instance.GetHall(), position);
            }
            UpdateMap(false);


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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < _roomArraySize.x; i++)
        {
            for(int j = 0; j < _roomArraySize.y; j++)
            {
                Gizmos.DrawSphere(_startRoomArrayPosition + new Vector3(j * _roomDistance,0, i * -_roomDistance), 0.5f);
            }
        }

        Gizmos.color= Color.green;
        Gizmos.DrawSphere(_startRoomArrayPosition + new Vector3(Mathf.RoundToInt(_roomArraySize.y / 2f) * _roomDistance, 0, -3.5f), 0.5f);
    }
}
