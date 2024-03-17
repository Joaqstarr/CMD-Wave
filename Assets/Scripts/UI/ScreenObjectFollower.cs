using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenObjectFollower : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    private WorldspaceObjectToFollow _objectToFollow;
    private RectTransform _rectTransform;
    private bool _initialized = false;
    [SerializeField]
    private int _visibleWithin = 80;

    [SerializeField]
    private TweenData _enterTweenData;
    [SerializeField]
    private TweenData _exitTweenData;
    private bool _isVisible = false;

    private CanvasGroup _group;
    // Start is called before the first frame update

    public void Initialize(WorldspaceObjectToFollow objToFollow)
    {
        _rectTransform = GetComponent<RectTransform>();
       _objectToFollow = objToFollow;
        _image.sprite = _objectToFollow.UiImage;
        _initialized = true;
        _group = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (!_initialized) return;
        if (_objectToFollow == null) Destroy(this);
        Vector2 pos = ScreenUtility.Instance.WorldToScreenPoint(_objectToFollow.transform.position);
        _rectTransform.anchoredPosition = pos;

        UpdateVisibility();

    }

    private void UpdateVisibility()
    {
        if (_objectToFollow.IsVisible && Mathf.Abs(_rectTransform.anchoredPosition.x) < _visibleWithin && Mathf.Abs(_rectTransform.anchoredPosition.y) < _visibleWithin)
        {
            if (!_isVisible)
            {
                transform.DOComplete();
                transform.localScale = Vector3.zero;
                _group.alpha = 1;
                transform.DOScale(Vector3.one, _enterTweenData.Duration).SetEase(_enterTweenData.Ease);
                _isVisible = true;
            }

        }
        else
        {
            if (_isVisible)
            {
                transform.DOComplete();

                transform.localScale = Vector3.one;
                _isVisible = false;

                transform.DOScale(Vector3.zero, _exitTweenData.Duration).SetEase(_exitTweenData.Ease).onComplete += () => { _group.alpha = 0; };
            }

        }
    }
    
}
