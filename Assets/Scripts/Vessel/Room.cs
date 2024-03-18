using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class Room : MonoBehaviour, IDataPersistance
{
    [SerializeField]
    private string _roomTag;
    [SerializeField]
    private string _roomName;

    [SerializeField]
    private Room _attachedUp;
    [SerializeField]
    private Room _attachedLeft;
    [SerializeField]
    private Room _attachedRight;
    [SerializeField]
    private Room _attachedDown;
    [SerializeField]
    private int pos;
    [SerializeField]
    private CommandBase[] _commandsOnRoom = new CommandBase[0];
    [SerializeField]
    private bool _staticRoom;


    [SerializeField]
    private Door _upDoor;
    [SerializeField]
    private Door _downDoor;
    [SerializeField]
    private Door _leftDoor;
    [SerializeField]
    private Door _rightDoor;
    private int _dmg;

    
    private RepairPoint[] _damages;
    private void Awake()
    {
        if(_roomTag.Length < 2)
        {
            _roomTag = " " + _roomTag;
        }
        else
        {
           // _roomTag = _roomTag.Substring(0, 3);
        }
        _damages = GetComponentsInChildren<RepairPoint>();

        InvokeRepeating("UpdateDoors", 1f, 1f);
    }

    public void UpdateDoors()
    {
        if(_upDoor != null)
            if (_attachedUp != null)
                _upDoor.Open();
            else
                _upDoor.Close();

        if (_downDoor != null)
            if (_attachedDown != null)
                _downDoor.Open();
            else
                _downDoor.Close();

        if (_leftDoor != null)
            if (_attachedLeft != null)
                _leftDoor.Open();
            else
                _leftDoor.Close();

        if (_rightDoor != null)
            if (_attachedRight != null)
                _rightDoor.Open();
            else
                _rightDoor.Close();

    }



    public int Position { get { return pos; } set { pos = value; } }

    public Vector2Int PositionVector
    {
        get
        {
            return new Vector2Int(Mathf.FloorToInt(pos / 10f), pos % 10);
        }
    }
    public string RoomTag {  get { return _roomTag; } }

    public string RoomName { get { return _roomName; } }

    public Room Left { get { return _attachedLeft; } set { _attachedLeft = value; } }
    public Room Right { get { return _attachedRight; } set { _attachedRight = value; } }
    public Room Down { get { return _attachedDown; } set { _attachedDown = value; } }

    public Room Up { get { return _attachedUp; } set { _attachedUp = value; } }

    public CommandBase[] AssociatedCommands
    {
        get { 
            return _commandsOnRoom;
        }
    }

    public bool IsStatic
    {
        get { return _staticRoom; }
    }

    public int RoomConnectedCount
    {
        get
        {
            int count = 0;
            if(_attachedDown)
                count++;
            if(_attachedLeft)
                count++;
            if(_attachedRight)
                count++;
            if (_attachedUp)
                count++;
            return count;
        }
    }

    public void ClearAttachments()
    {
        _attachedLeft = null;
        _attachedRight = null;
        _attachedUp = null; 
        _attachedDown = null;
    }

    public bool Attached
    {
        get { return _attachedLeft != null || _attachedRight != null || _attachedUp != null || _attachedDown != null; }
    }

    public void Damage()
    {
        if(_damages.Length > 0 ) { }
            _damages[Random.Range(0, _damages.Length)].Damage();
    }

    public void SaveData(ref SaveData data)
    {
        if (RoomTag == "" || RoomTag == string.Empty || RoomTag == " ") return;


        byte result = 0;
        for(int i = 0; i < _damages.Length; i++)
        {
            if (_damages[i].IsDamaged)
                result |= (byte)(1 << i);
        }

        if (data._roomDamages == null)
            data._roomDamages = new SerializableDictionary<string, byte>();
        if(data._roomDamages.ContainsKey(RoomTag.ToUpper()))
        {
            data._roomDamages[RoomTag.ToUpper()] = result;
        }
        else
        {
            data._roomDamages.Add(RoomTag.ToUpper().ToUpper(), result);

        }
    }

    public void LoadData(SaveData data)
    {
        if (RoomTag == "" || RoomTag == string.Empty || RoomTag == " ") return;
        byte damages = data._roomDamages[RoomTag.ToUpper()];

        
        for (int i = 0; i < _damages.Length; i++)
        {
            if((damages << i) == 1)
                _damages[i].Damage();
            else
                _damages[i].Repair();
        }
    }

    public int DamageAmount
    {
        get {
            int amt = 0;
            if (_damages == null)
                return 0;
            for (int i = 0; i < _damages.Length; i++)
                if (_damages[i].IsDamaged)
                    amt++;

            return amt; 
        }
    }



}
