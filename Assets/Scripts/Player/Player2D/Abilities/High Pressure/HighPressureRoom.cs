using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPressureRoom : MonoBehaviour
{
    private Room _roomToRead;
    public static bool HighPressureEquipped;
    // Start is called before the first frame update
    void Start()
    {
        _roomToRead = GetComponent<Room>();
    }

    // Update is called once per frame
    void Update()
    {
        HighPressureEquipped = _roomToRead.Attached;
    }
}
