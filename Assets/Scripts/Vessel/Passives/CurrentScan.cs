using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CurrentScan : MonoBehaviour
{
    private Room _roomToRead;
    public static bool CurrentScanEquipped;
    // Start is called before the first frame update
    void Start()
    {
        _roomToRead = GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        CurrentScanEquipped = _roomToRead.Attached;
    }
}
