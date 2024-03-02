using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Texture2D fogOfWarTexture;
    public SpriteRenderer spriteRend;
    [SerializeField] private float _holeRadius = 5;
    public Transform _player;
    private Vector2 worldScale;
    private Vector2Int pixelScale;
    [SerializeField]
    private bool _resetTexture = false;
    public void Awake()
    {
        pixelScale.x = fogOfWarTexture.width;
        pixelScale.y = fogOfWarTexture.height;
        worldScale.x = pixelScale.x / 100f * transform.localScale.x;
        worldScale.y = pixelScale.y / 100f * transform.localScale.y;
        if (_resetTexture)
        {
            for (int i = 0; i < pixelScale.x; i++)
            {
                for (int j = 0; j < pixelScale.y; j++)
                {
                    fogOfWarTexture.SetPixel(i, j, Color.black);
                }
            }
            fogOfWarTexture.Apply();
            CreateSprite();
        }
        InvokeRepeating("CreateHole", 1, 1);
    }

    

    private Vector2Int WorldToPixel(Vector2 position)
    {
        Vector2Int pixelPosition = Vector2Int.zero;

        float dx = position.x - transform.position.x;
        float dy = position.y - transform.position.y;

        pixelPosition.x = Mathf.RoundToInt(.5f * pixelScale.x + dx * (pixelScale.x / worldScale.x));
        pixelPosition.y = Mathf.RoundToInt(.5f * pixelScale.y + dy * (pixelScale.y / worldScale.y));
        return pixelPosition;
         
    }

    private void CreateHole()
    {
        MakeHole(_player.position, _holeRadius);
    }
    public void MakeHole(Vector2 position, float holeRadius)
    {
        Vector2Int pixelPosition = WorldToPixel(position);
        int radius = Mathf.RoundToInt(holeRadius * pixelScale.x / worldScale.x);
        int px, nx, py, ny, distance;
        for (int i = 0; i < radius; i++)
        {
            distance = Mathf.RoundToInt(Mathf.Sqrt(radius * radius - i * i));
            for (int j = 0; j < distance; j++)
            {
                px = Mathf.Clamp(pixelPosition.x + i, 0, pixelScale.x);
                nx = Mathf.Clamp(pixelPosition.x - i, 0, pixelScale.x);
                py = Mathf.Clamp(pixelPosition.y + j, 0, pixelScale.y);
                ny = Mathf.Clamp(pixelPosition.y - j, 0, pixelScale.y);

                fogOfWarTexture.SetPixel(px, py, Color.clear);
                fogOfWarTexture.SetPixel(nx, py, Color.clear);
                fogOfWarTexture.SetPixel(px, ny, Color.clear);
                fogOfWarTexture.SetPixel(nx, ny, Color.clear);
            }
        }
        fogOfWarTexture.Apply();
        CreateSprite();
    }

    private void CreateSprite()
    {

        spriteRend.sprite = Sprite.Create(fogOfWarTexture, new Rect(0, 0, fogOfWarTexture.width, fogOfWarTexture.height), Vector2.one * .5f, 100);
    }
}
