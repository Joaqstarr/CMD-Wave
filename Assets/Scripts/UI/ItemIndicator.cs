using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ItemIndicator : MonoBehaviour
{
    private Item _linkedItem;
    private bool _initialized = false;
    [SerializeField]
    private TMP_Text _label;
    CanvasGroup _group;
    private RectTransform _rectTransform;
    [SerializeField]
    private TweenData _enterTweenData;
    [SerializeField]
    private TweenData _exitTweenData;

    private bool _shouldBeVisible = false;
    private bool _isVisible = false;
    public void Initialize(Item linkedItem)
    {
        _initialized = true;
        _linkedItem = linkedItem;
        _group = GetComponent<CanvasGroup>();
        _group.interactable = false;
        _group.blocksRaycasts = false;

        linkedItem.ItemSpawned += OnSpawn;
        linkedItem.ItemDespawned += OnDespawn;
        _rectTransform = GetComponent<RectTransform>();

        if (_linkedItem.gameObject.activeInHierarchy)
        {
            OnSpawn(_linkedItem.RoomCode);
        }
        else
        {
            OnDespawn(_linkedItem.RoomCode);
        }
    }

    private void OnDisable()
    {
        if( _initialized)
        {

            _linkedItem.ItemSpawned -= OnSpawn;
            _linkedItem.ItemDespawned -= OnDespawn;
        }
    }

    void Update()
    {
        if (!_initialized) return;
        Vector2 pos = ScreenUtility.Instance.WorldToScreenPoint(_linkedItem.transform.position);
        _rectTransform.anchoredPosition = pos;

        UpdateVisibility();

    }

    private void OnSpawn(string key) {
        _shouldBeVisible = true;
        _label.text = key;

    }

    private void OnDespawn(string key)
    {

        _shouldBeVisible = false;
    }

    private void UpdateVisibility()
    {
        if (_shouldBeVisible && Mathf.Abs(_rectTransform.anchoredPosition.x) < 110 && Mathf.Abs(_rectTransform.anchoredPosition.y) < 110)
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
            if(_isVisible)
            {
                transform.DOComplete();

                transform.localScale = Vector3.one;
                _isVisible = false;

                transform.DOScale(Vector3.zero, _exitTweenData.Duration).SetEase(_exitTweenData.Ease).onComplete += () => { _group.alpha = 0; };
            }

        }
    }
}
