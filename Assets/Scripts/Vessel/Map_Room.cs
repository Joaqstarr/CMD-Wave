using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Map_Room : MonoBehaviour
{
    [SerializeField]
    private string _roomTag;
    [SerializeField]
    private int _location;
    [SerializeField]
    private Image _roomImage;
    [SerializeField]
    private Sprite _selectableSprite;
    [SerializeField]
    private Sprite _inUseSprite;
    [SerializeField]
    private Sprite _hiddenSprite;

    [SerializeField]
    private Map_Room _onUp;
    [SerializeField]
    private Map_Room _onDown;
    [SerializeField]
    private Map_Room _onLeft;
    [SerializeField]
    private Map_Room _onRight;

    TMP_Text _label;
    enum RoomStates { 
        Hidden,
        Selectable,
        InUse
    }
    [SerializeField]
    private RoomStates _state;
    private void Awake()
    {
        _label = GetComponentInChildren<TMP_Text>();

    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateLabelText();
    }


    private void UpdateLabelText()
    {
        switch(_state )
        {
            case RoomStates.Hidden:
                _label.text = " ";
                _roomImage.sprite = _hiddenSprite;
                break; 
            case RoomStates.Selectable:
                _label.text = _location.ToString("D2");
                _roomImage.sprite = _selectableSprite;
                break;
            case RoomStates.InUse:
                _label.text = _roomTag;
                _roomImage.sprite = _inUseSprite;
                break;
        }
    }

    public void Activate(string key = null)
    {
        if(key != null)
            _roomTag = key;
        _state = RoomStates.InUse;
        UpdateLabelText() ;
    }
    public void MakeSelectable()
    {
        _roomTag = _location.ToString();
        _state = RoomStates.Selectable;
        UpdateLabelText();


    }
    public void Deactivate()
    {
        _state = RoomStates.Hidden;
        UpdateLabelText() ;
    }
    public bool Activated
    {
        get { return _state == RoomStates.InUse;}
    }
    public bool IsSelectable
    {
        get { return _state == RoomStates.Selectable; }
    }
    public int Position
    {
        get { return _location; }
    }
    public Vector2Int PositionVector
    {
        get
        {
            return new Vector2Int(Mathf.FloorToInt(_location / 10f), _location % 10);
        }
    }
    public Map_Room OnRight
    {
        get { return _onRight; }
    }

    public Map_Room OnLeft
    {
        get { return _onLeft; }
    }
    public Map_Room OnDown
    {
        get { return _onDown; }
    }
    public Map_Room OnUp
    {
        get { return _onUp; }
    }
}
