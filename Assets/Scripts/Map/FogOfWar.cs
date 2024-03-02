using DG.Tweening;
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


    public static FogOfWar Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
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
        //InvokeRepeating("CreateHole", 1, 1);
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

    public void MakeTriangle(Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2Int pixelPosA = WorldToPixel(a);
        Vector2Int pixelPosB = WorldToPixel(b);
        Vector2Int pixelPosC = WorldToPixel(c);


        int minX = Mathf.Min(pixelPosA.x, Mathf.Min(pixelPosB.x, pixelPosC.x));
        int maxX = Mathf.Max(pixelPosA.x, Mathf.Max(pixelPosB.x, pixelPosC.x));
        int minY = Mathf.Min(pixelPosA.y, Mathf.Min(pixelPosB.y, pixelPosC.y));
        int maxY = Mathf.Max(pixelPosA.y, Mathf.Max(pixelPosB.y, pixelPosC.y));

        for (int x = minX; x <= maxX; x++)
        {
            for (int y = minY; y <= maxY; y++) {
                if (IsPointInTriangle(new Vector2Int(x, y), pixelPosA, pixelPosB, pixelPosC))
                { 
                    fogOfWarTexture.SetPixel(x, y, Color.clear);

                }
            }
        }

        fogOfWarTexture.Apply();
        CreateSprite();
    }

    private bool IsPointInTriangle(Vector2Int pt, Vector2Int v1, Vector2Int v2, Vector2Int v3)
    {
        bool b1 = Sign(pt, v1, v2) < 0.0f;
        bool b2 = Sign(pt, v2, v3) < 0.0f;
        bool b3 = Sign(pt, v3, v1) < 0.0f;
        return (b1 == b2) && (b2 == b3);
    }

    private float Sign(Vector2Int p1, Vector2Int p2, Vector2Int p3)
    {
        return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
    }

    private void CreateSprite()
    {
        spriteRend.sprite = Sprite.Create(fogOfWarTexture, new Rect(0, 0, fogOfWarTexture.width, fogOfWarTexture.height), Vector2.one * .5f, 100,0,SpriteMeshType.FullRect);
    }
}
