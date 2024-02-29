using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    private string _roomTag;
    [SerializeField]
    private string _roomName;

    [SerializeField]
    private Room _attachedLeft;
    [SerializeField]
    private Room _attachedRight;
    [SerializeField]
    private Room _attachedDown;
    [SerializeField]
    private int pos;
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
    }
    public void AttachRoom()
    {

    }

    public int Position { get { return pos; } }
    public string RoomTag {  get { return _roomTag; } }


}
