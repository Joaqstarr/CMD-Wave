using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject _doorMesh;
    [SerializeField]
    private GameObject _openDoorMesh;
    [SerializeField]
    private Light _indicatorLight;

    [SerializeField]
    private Color _openColor;
    [SerializeField]
    private Color _closeColor;
    public void Open()
    {
        if(_openDoorMesh != null)
        {
            _openDoorMesh.SetActive(true);
        }
        _doorMesh.SetActive(false);
        if(_indicatorLight != null)
        {
            _indicatorLight.color = _openColor;

        }

    }
    private void Awake()
    {
        Close();
    }
    public void Close()
    {
        if (_openDoorMesh != null)
        {
            _openDoorMesh.SetActive(false);
        }
        _doorMesh.SetActive(true);
        if (_indicatorLight != null)
        {
            _indicatorLight.color = _closeColor;
        }
    }
}
