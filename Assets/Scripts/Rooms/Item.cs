using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour
{
    public delegate void ItemDelegate(string key);
    public ItemDelegate ItemSpawned;
    public ItemDelegate ItemDespawned;
    [SerializeField]
    private string _itemTag;

    [SerializeField]
    private Color _gizmoColor = Color.green;

    public string RoomCode{
        get { return _itemTag; }
        set { _itemTag = value; }
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        if (ItemDespawned != null)
        {
            ItemDespawned(_itemTag);
        }
    }

    public void Spawn(string roomCode)
    {
        _itemTag = roomCode;
        gameObject.SetActive(true);
        if(ItemSpawned != null)
        {
            ItemSpawned(_itemTag);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;
        Gizmos.DrawSphere(transform.position, 1f);
        Handles.Label(transform.position, _itemTag);
    }


}
