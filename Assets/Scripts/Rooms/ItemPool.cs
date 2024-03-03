using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private List<Item> _items = new List<Item>();
    public static ItemPool Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _items = GetComponentsInChildren<Item>().ToList<Item>();

    }


    public Item GetFreeItem(string newKey)
    {
        for(int i = 0; i < _items.Count; i++)
        {
            if (_items[i].gameObject.activeInHierarchy == false)
            {
                _items[i].RoomCode = newKey;
                return _items[i];
            }
        }

        return null;
    }
    public List<Item> ItemList { get { return _items; } }
}
