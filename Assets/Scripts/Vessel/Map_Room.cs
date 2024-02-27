using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // Update is called once per frame
    void Update()
    {
        
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
}
