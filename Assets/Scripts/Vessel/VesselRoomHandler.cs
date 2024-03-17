using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VesselRoomHandler : MonoBehaviour, IDataPersistance
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
    private Vector2Int[] _roomPositions;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _roomPositions = new Vector2Int[(_roomArraySize.x-1) * _roomArraySize.y];

        int f = 0;
        for (int i = 1; i <= _roomPositions.Length / _roomArraySize.y; i++)
        {
            for(int j = 0; j < _roomPositions.Length/(_roomArraySize.x-1); j++, f++)
            {
                _roomPositions[f] = new Vector2Int(i, j);
            }
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
        if (PlacedRooms.ContainsKey(position))
        {
            PlacedRooms[position].transform.parent = RoomPool.Instance.transform;
            PlacedRooms[position].transform.localPosition = Vector3.zero;
            PlacedRooms.Remove(position);
        }
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
    public bool RemoveRoom(string roomTag, out string keyRemoved)
    {
        keyRemoved = "";
        Room roomToDrop = GetRoom(roomTag);
        if (roomToDrop == null)
            return false;

        return RemoveRoom(roomToDrop.PositionVector, out keyRemoved);
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

            if(roomRemoving.Right != null)
            {
                roomRemoving.Right.Left = null;
            }
            if (roomRemoving.Left != null)
            {
                roomRemoving.Left.Right = null;
            }
            if (roomRemoving.Up != null)
            {
                roomRemoving.Up.Down = null;
            }
            if (roomRemoving.Down != null)
            {
                roomRemoving.Down.Up = null;
            }



            if (roomRemoving.RoomConnectedCount > 1)
            {
                AddRoom(RoomPool.Instance.GetHall(), position);
            }

            roomRemoving.ClearAttachments();

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

    public void SaveData(ref SaveData data)
    {
        UpdateRooms();

        data._roomPositions = new SerializableDictionary<string, Vector2Int>();
        data._roomPlaces = new SerializableDictionary<Vector2Int, string>();

        for(int i = 0; i <_rooms.Length; i++)
        {
            if (!_rooms[i].IsStatic)
            {
                data._roomPositions.Add(_rooms[i].RoomTag, _rooms[i].PositionVector);
                data._roomPlaces.Add(_rooms[i].PositionVector, _rooms[i].RoomTag);

            }
        }

    }
    private void ClearRooms()
    {
        RoomsContext.Clear();
        PlacedRooms.Clear();
        
        UpdateRooms();
        AddRoom(_commandRoom, new Vector2Int(0, 2));
        for(int i = 0; i < _rooms.Length; i++)
        {
            foreach(Room room in _rooms)
            {
                if (!room.IsStatic)
                {
                    room.transform.parent = RoomPool.Instance.transform;
                    room.ClearAttachments();

                }
            }
        }
    }

    private Room GetRoom(string roomTag)
    {
        for (int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].RoomTag.ToUpper() == roomTag.ToUpper())
                return _rooms[i];
        }
        return null;
    }
    public int AmountOfRooms(string roomTag)
    {
        UpdateRooms();
        int amt = 0;

        for(int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].RoomTag.ToUpper() == roomTag.ToUpper())
                amt++;
        }

        return amt;
    }
    public void LoadData(SaveData data)
    {

        ClearRooms();

        for (int i = 0; i < _roomPositions.Length; i++)
        {
            if (data._roomPlaces.ContainsKey(_roomPositions[i]))
            {
                AddRoom(RoomPool.Instance.GetRoom(data._roomPlaces[_roomPositions[i]]), _roomPositions[i]);
            }
        }
    }

    public void DamageRoom()
    {
        if(_rooms.Length == 0) return;

        int randomRoom = Random.Range(0, _rooms.Length);
        _rooms[randomRoom].Damage();
        UpdateMapColors();
    }

    public void UpdateMapColors()
    {
        Map.Instance.UpdateMapColors(_rooms);
    }
    public int DamageAmount
    {
        get
        {
            int dmg = 0;
            for(int i = 0; i < _rooms.Length; i++)
                dmg += _rooms[i].DamageAmount;

            return dmg;
        }
    }

    private void NewGame()
    {
        ClearRooms();
        AddRoom(RoomPool.Instance.GetRoom("DO"), new Vector2Int(1, 2));
    }

    private void OnEnable()
    {
        GameStartManager.GameStarted += NewGame;
    }

    private void OnDisable()
    {
        GameStartManager.GameStarted -= NewGame;

    }

    public CommandBase[] GetCommandsFromKey(string key)
    {
        for(int i = 0; i < _rooms.Length; i++)
        {
            if (_rooms[i].RoomTag.ToLower() == key.ToLower())
            {
                return _rooms[i].AssociatedCommands;
            }
        }
        return null;
    }
}
