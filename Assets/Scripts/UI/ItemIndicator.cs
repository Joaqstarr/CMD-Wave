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
    public void Initialize(Item linkedItem)
    {
        _initialized = true;
        _linkedItem = linkedItem;
        _group = GetComponent<CanvasGroup>();
        _group.interactable = false;
        _group.blocksRaycasts = false;

        linkedItem.ItemSpawned += OnSpawn;
        linkedItem.ItemDespawned += OnDespawn;
        _label.text = _linkedItem.RoomCode;
        _rectTransform = GetComponent<RectTransform>();
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

    }

    private void OnSpawn(string key) {
        _group.alpha = 1;
        _label.text = key;
    }

    private void OnDespawn(string key)
    {
        _group.alpha = 0;

    }
}
