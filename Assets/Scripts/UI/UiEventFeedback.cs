using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiEventFeedback : MonoBehaviour, IPointerEnterHandler
{
    private RectTransform _rectTransform;
    [SerializeField]
    private TweenData _hoverTween;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rectTransform.DOComplete();
        _rectTransform.DOShakeAnchorPos(_hoverTween.Duration, _hoverTween.Strength, 10, 90, false, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }


}
