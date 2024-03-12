using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public float[] _subPosition = new float[2];
    public int _subHealth;

    public string _imageName = "fogImage.png";
    public Texture2D _fogOfWarTexture;

    public SerializableDictionary<string, Vector2Int> _roomPositions;
    public SerializableDictionary<Vector2Int, string> _roomPlaces;

    public SerializableDictionary<string, Vector2> _itemPositions;
}
