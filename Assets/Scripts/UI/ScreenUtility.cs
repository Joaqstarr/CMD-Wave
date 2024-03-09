using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenUtility : MonoBehaviour
{

    [SerializeField]
    private Camera _2dCamera;

    public static ScreenUtility Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public Vector2 WorldToScreenPoint(Vector2 position)
    {
        Vector2 screenPos = _2dCamera.WorldToScreenPoint(position);
        screenPos.x -= _2dCamera.pixelWidth/2;
        screenPos.y -= _2dCamera.pixelHeight / 2;
        Debug.Log(screenPos);

        return screenPos;
    }
}
