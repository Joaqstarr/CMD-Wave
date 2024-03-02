using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    private MapControls _controls;
    [SerializeField]
    private float _speed = 10f;
    private void Start()
    {
        _controls = GetComponent<MapControls>();
    }
    void Update()
    {
        transform.Translate(_controls.MoveInput * _speed *  Time.deltaTime);
    }

}
