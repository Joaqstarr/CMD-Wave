using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        _label = GetComponentInChildren<TMP_Text>();
        UpdateLabelText();
    }


    private void UpdateLabelText()
    {
        switch(_state )
        {
            case RoomStates.Hidden:
                _label.text = "";
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

        _state = RoomStates.Selectable;
        
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
    public int Position
    {
        get { return _location; }
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
