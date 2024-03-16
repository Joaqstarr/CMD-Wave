using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.UIElements;

public class FogOfWar : MonoBehaviour, IDataPersistance
{
    public Texture2D fogOfWarTexture;
    public SpriteRenderer spriteRend;
    private Vector2 worldScale;
    private Vector2Int pixelScale;
    [SerializeField]
    private bool _resetTexture = false;

    [SerializeField]
    private Vector2Int _textureSize;
    [SerializeField]
    private float _coneFadeRemap = 0.7f;
    public static FogOfWar Instance;

    private NativeArray<Color32> PixelArray;

    private JobHandle HandleJob;



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

        if (_resetTexture) 
            fogOfWarTexture.Reinitialize(_textureSize.x, _textureSize.y);

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
    struct MakeFogHole : IJob
    {
        public Vector2Int pixelPosition;
        public int radius;
        public NativeArray<Color32> Pixels;
        public Vector2Int _pixelScale;
        public Vector2Int TextureSize;
        public float alpha;
        public void Execute()
        {
            int px, nx, py, ny, distance;
            for (int i = 0; i < radius; i++)
            {
                distance = Mathf.RoundToInt(Mathf.Sqrt(radius * radius - i * i));
                for (int j = 0; j < distance; j++)
                {
                    px = Mathf.Clamp(pixelPosition.x + i, 0, _pixelScale.x);
                    nx = Mathf.Clamp(pixelPosition.x - i, 0, _pixelScale.x);
                    py = Mathf.Clamp(pixelPosition.y + j, 0, _pixelScale.y);
                    ny = Mathf.Clamp(pixelPosition.y - j, 0, _pixelScale.y);



                    Color pColor = GetPixel(px, py);

                    pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance) * Mathf.Cos((float)i / radius)));
                    SetPixel(px, py, pColor);

                    pColor = GetPixel(nx, py);
                    pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance) * Mathf.Cos((float)i / radius)));
                    SetPixel(nx, py, pColor);

                    pColor = GetPixel(px, ny);
                    pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance) * Mathf.Cos((float)i / radius)));
                    SetPixel(px, ny, pColor);

                    pColor = GetPixel(nx, ny);
                    pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance) * Mathf.Cos((float)i / radius)));
                    SetPixel(nx, ny, pColor);
                }

            }

            
        }

        private Color32 GetPixel(int x, int y)
        {
            return Pixels[(y * TextureSize.x) + x];
        }
        private void SetPixel(int x, int y, Color32 pixel)
        {
            Pixels[(y * TextureSize.x) + x] = pixel;
        }
    }
    public void MakeHole(Vector2 position, float holeRadius, float alphaToSet = 0)
    {
        /*
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

                Color pColor = fogOfWarTexture.GetPixel(px, py);

                pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance)* Mathf.Cos((float)i / radius)));
                fogOfWarTexture.SetPixel(px, py, pColor );

                pColor = fogOfWarTexture.GetPixel(nx, py);
                pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance) * Mathf.Cos((float)i / radius))) ;
                fogOfWarTexture.SetPixel(nx, py, pColor );

                pColor = fogOfWarTexture.GetPixel(px, ny);
                pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1,Mathf.Sin((float)j / distance)* Mathf.Cos((float)i / radius)));
                fogOfWarTexture.SetPixel(px, ny, pColor );
                
                pColor = fogOfWarTexture.GetPixel(nx, ny);
                pColor.a = Mathf.Min(pColor.a, Mathf.Lerp(alpha, 1, Mathf.Sin((float)j / distance)* Mathf.Cos((float)i / radius))) ;
                fogOfWarTexture.SetPixel(nx, ny, pColor );
            }
        }
        */

        PixelArray = fogOfWarTexture.GetRawTextureData<Color32>();
        MakeFogHole fogMultithread = new MakeFogHole()
        {
           pixelPosition = WorldToPixel(position),
           radius = Mathf.RoundToInt(holeRadius * pixelScale.x / worldScale.x),
           Pixels = PixelArray,
           _pixelScale = pixelScale,
           TextureSize = _textureSize,
           alpha = alphaToSet
        };
        HandleJob = fogMultithread.Schedule();
        fogOfWarTexture.Apply();
        StartCoroutine(CompleteJob());
    }


    IEnumerator CompleteJob()
    {
        yield return new WaitForEndOfFrame();
        HandleJob.Complete();
        CreateSprite();

    }
    public void MakeTriangle(Vector2 a, Vector2 b, Vector2 c, float alpha = 0)
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
                Vector2Int pixelCheckPos = new Vector2Int(x, y);
                if (IsPointInTriangle(pixelCheckPos, pixelPosA, pixelPosB, pixelPosC))
                {
                    
                    Color pixelColor = fogOfWarTexture.GetPixel(x, y);


                    Vector2 closestPoint = FindNearestPointOnLine(pixelPosA, ((pixelPosB + pixelPosC) /2)-pixelPosA, pixelCheckPos);

                    Vector2 furthestPoint;
                    if(Vector2.Distance(pixelCheckPos, pixelPosB) < Vector2.Distance(pixelCheckPos, pixelPosC))
                        furthestPoint = FindNearestPointOnLine(pixelPosA, pixelPosB - pixelPosA, pixelCheckPos);
                    else
                        furthestPoint = FindNearestPointOnLine(pixelPosA, pixelPosC - pixelPosA, pixelCheckPos);


                    float distance = Vector2.Distance(pixelCheckPos, closestPoint);
                    distance = distance / Vector2.Distance(furthestPoint, closestPoint);


                    distance -= _coneFadeRemap;
                    distance = Mathf.Clamp01(distance);
                    distance *= 1 / (1-_coneFadeRemap);
                    
                    pixelColor.a = Mathf.Min(pixelColor.a, Mathf.Lerp( alpha, 1, distance));


                   // if(new Vector2Int(Mathf.RoundToInt(furthestPoint.x), Mathf.RoundToInt(y)) == pixelCheckPos)
                  //  fogOfWarTexture.SetPixel(Mathf.RoundToInt( furthestPoint.x), Mathf.RoundToInt(y), Color.red);
                  //  else
                    
                    fogOfWarTexture.SetPixel(x, y, pixelColor);


                }
            }
        }

        fogOfWarTexture.Apply();
        CreateSprite();
    }


    public Vector2 FindNearestPointOnLine(Vector2 origin, Vector2 direction, Vector2 point)
    {
        direction.Normalize();
        Vector2 lhs = point - origin;

        float dotP = Vector2.Dot(lhs, direction);
        return origin + direction * dotP;
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

    public void SaveData(ref SaveData data)
    {
        data._fogOfWarTexture = fogOfWarTexture;

        
    }

    public void LoadData(SaveData data)
    {
        if (data._fogOfWarTexture == null) return;

        fogOfWarTexture.SetPixels(data._fogOfWarTexture.GetPixels());
        fogOfWarTexture.Apply();
    }
}
