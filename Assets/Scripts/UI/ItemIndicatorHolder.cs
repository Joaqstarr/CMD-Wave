using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIndicatorHolder : MonoBehaviour
{
    [SerializeField] private ItemPool _pool;
    [SerializeField] private ItemIndicator _itemIndicatorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        List<Item> list = _pool.ItemList;

        for (int i = 0; i < list.Count; i++)
        {
            ItemIndicator spawnedIndicator = Instantiate(_itemIndicatorPrefab, transform);
            spawnedIndicator.Initialize(list[i]);
        }
    }


}
