using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField]
    private string _itemTag;

    public string RoomCode{
        get { return _itemTag; }
        set { _itemTag = value; }
    }

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}
