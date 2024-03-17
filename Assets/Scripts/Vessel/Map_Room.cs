using DG.Tweening;
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
    private bool _canBeSelected = true;
    [SerializeField]
    private Map_Room _onUp;
    [SerializeField]
    private Map_Room _onDown;
    [SerializeField]
    private Map_Room _onLeft;
    [SerializeField]
    private Map_Room _onRight;

    [SerializeField]
    private RectTransform _transform;
    [SerializeField]

    TMP_Text _label;
    [Header("Colors")]
    [SerializeField]private Color _undamagedColor = Color.white;
    [SerializeField] private Color _damagedColor = Color.red;

    [Header("Tweens")]
    [SerializeField] private TweenData _appearTween;
    [SerializeField] private TweenData _disapearTween;

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
        _transform = GetComponent<RectTransform>();

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
        {
            _roomTag = key;
            _roomTag = _roomTag.Replace(" ", string.Empty);
        }
        if(_state != RoomStates.InUse)
        {
            _transform.DOComplete();
            _transform.DOShakePosition(_appearTween.Duration, _appearTween.Strength, 9, 90, false, true).SetEase(_appearTween.Ease);

        }
        _state = RoomStates.InUse;
        UpdateLabelText() ;
    }
    public void MakeSelectable()
    {
        if (!_canBeSelected) return;
        _roomTag = _location.ToString();

        if (_state != RoomStates.Selectable)
        {
            _transform.DOComplete();

            _transform.DOShakePosition(_disapearTween.Duration, _disapearTween.Strength, 9, 90,false, true);

        }

        _state = RoomStates.Selectable;
        UpdateLabelText();


    }
    public void Deactivate()
    {

        if (_state != RoomStates.Hidden)
        {
            _transform.DOComplete();

            _transform.DOShakePosition(_disapearTween.Duration, _disapearTween.Strength, 9, 90, false,true);

        }

        _state = RoomStates.Hidden;
        UpdateLabelText() ;
    }

    public void SetColorByDamage(int damage)
    {
        if(damage > 0)
        {
            _roomImage.color = _damagedColor;
            _transform.DOComplete();
            _transform.DOShakePosition(_appearTween.Duration, _appearTween.Strength, 9, 90, false, true).SetEase(_appearTween.Ease);
        }
        else
        {
            _roomImage.color = _undamagedColor;
        }
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
    public string RoomTag
    {
        get { return _roomTag; }
    }
}
