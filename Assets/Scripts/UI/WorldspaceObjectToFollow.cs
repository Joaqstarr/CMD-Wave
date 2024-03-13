using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceObjectToFollow : MonoBehaviour
{
    [SerializeField]
    private Sprite _uiImage;
    private void Awake()
    {
        ScreenspaceFollowerManager._worldspaceObjectToFollows.Add(this);
    }
    public Sprite UiImage { get { return _uiImage; } }
    public bool IsVisible { get { return gameObject.activeInHierarchy; } }
}
