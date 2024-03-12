using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemPool : MonoBehaviour, IDataPersistance
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

    public void SaveData(ref SaveData data)
    {
        data._itemPositions = new SerializableDictionary<string, Vector2>();

        foreach (Item item in _items)
        {
            if(item.gameObject.activeInHierarchy == true)
            {
                data._itemPositions[item.RoomCode] = item.transform.position;
            }
        }


    }

    public void LoadData(SaveData data)
    {

        foreach (Item item in _items)
        {
            item.gameObject.SetActive(true);

            if (data._itemPositions.ContainsKey(item.RoomCode))
            {
                item.transform.position = data._itemPositions[item.RoomCode];
                item.Spawn(item.RoomCode);
            }
            else
            {
                item.Collect();
            }
        }

    }

    public List<Item> ItemList { get { return _items; } }
}
