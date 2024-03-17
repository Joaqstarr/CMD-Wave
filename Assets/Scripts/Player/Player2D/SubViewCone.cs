using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class SubViewCone : MonoBehaviour
{
    public static float subAimAngle;
    public static Vector3 subAimVector;

    public PlayerSubData data;
    public ScanDartData scanDartData;

    public Camera camera2D; // camera for 2D terminal

    private Mesh _mesh; // mesh for cone polygons
    private Vector3 _origin; // starting point of cone - should be on player
    private float _fov; // how wide cone is in degrees
    private int _resolution; // how many polygons make up the cone mesh - smoothness
    private float _viewDistance; // how far out the cone goes
    private int _rayResolution; // resolution of rays cast for scanning collision
    private float _blipGhostTime; // how long blips stay visible after out of vision
    private int _sampleRate; // how many times a second the cone scans for collision - UNUSED
    private LayerMask _collisionMask; // layer mask for raycast scan collisions

    private Mesh _stencilMesh;

    private float _angleIncrease;
    private float _aimAngle;
    private float _angleRadians;
    private Vector3 _angleVector;
    private float _rayRadians;
    private Vector3 _rayVector;
    private int _vertexIndex;
    private int _triangleIndex;
    private bool _scanWaiting;

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;
    private Vector3[] _stencilVertices;
    private Vector2[] _stencilUv;
    private int[] _stencilTriangles;
    private MeshRenderer[] _rayCollisions;
    private MeshRenderer _pooledBlip;
    public static List<MeshRenderer> _blips;
    private int _numBlips;
    [SerializeField]
    private float _fogAlpha = 0.1f;
    [SerializeField]
    private LayerMask _terrainMask; // layer mask for raycast terrain collisions
    private MeshRenderer _renderer;
    private void Awake()
    {
        // Blip object pool
        GameObject holder = new GameObject();
        holder.name = "BlipHolder";
        _pooledBlip = data.blipObject.GetComponent<MeshRenderer>();
        _blips = new List<MeshRenderer>();
        _numBlips = (data.rayResolution + scanDartData.rayResolution * scanDartData.numToPool) * 2;
        MeshRenderer tempObject;
        for (int i = 0; i < _numBlips; i++)
        {
            tempObject = Instantiate(_pooledBlip);
            tempObject.gameObject.SetActive(false);
            tempObject.transform.SetParent(holder.transform);
            _blips.Add(tempObject);
        }

        InvokeRepeating("UpdateConeMat", 1f, 1f);
    }

    private void Start()
    {

        // variable assignments
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        GetComponent<MeshCollider>().sharedMesh = _mesh;
        _renderer = GetComponent<MeshRenderer>();

        _origin = transform.localPosition;
        _fov = data.fov;
        _resolution = data.resolution;
        _viewDistance = data.viewDistance;
        _rayResolution = data.rayResolution;
        //_blipGhostTime = data.blipGhostTime;
        _sampleRate = data.sampleRate;
        _collisionMask = data.collisionMask;

        _angleIncrease = _fov / _resolution;
        _scanWaiting = false;

        _vertices = new Vector3[_resolution + 2];
        _uv = new Vector2[_vertices.Length];
        _triangles = new int[_resolution * 3];
        _rayCollisions = new MeshRenderer[_rayResolution];

        _vertices[0] = _origin;

        // stencil mesh
        _stencilMesh = new Mesh();
        transform.Find("RadarStencil").GetComponent<MeshFilter>().mesh = _stencilMesh;

        _stencilVertices = new Vector3[_resolution + 2];
        _stencilUv = new Vector2[_stencilVertices.Length];
        _stencilTriangles = new int[_resolution * 3];


        if (FogOfWar.Instance != null)
        {
            InvokeRepeating("RepeatDrawFog", 0.1f, 0.1f);
        }
    }


    public void DrawViewCone(Vector3 aimPos)
    {

        // delete any blips from last frame
        if (_rayCollisions.Count() > 0)
            for (int i = 0; i < _rayCollisions.Count(); i++)
            {
                if (_rayCollisions[i] != null)
                {
                    StartCoroutine(BlipGhostEffect(_rayCollisions[i].gameObject));
                    _rayCollisions[i] = null;
                }
            }    


        // convert mouse position to angle
        if (camera2D == null)
            aimPos = Camera.main.ScreenToWorldPoint(aimPos);
        else
            aimPos = ConvertAimCoordinate(aimPos);

        aimPos = (aimPos - transform.position).normalized;
        aimPos.z = 0;

        _aimAngle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        if (_aimAngle < 0) _aimAngle += 360;

        // set static angle variables
        subAimAngle = _aimAngle;
        float tempRadians = _aimAngle * (Mathf.PI / 180f);
        subAimVector = new Vector3(Mathf.Cos(tempRadians), Mathf.Sin(tempRadians));

        float angle = _aimAngle + (_fov/2f);

        //render mesh
        _triangleIndex = 0;
        _vertexIndex = 1;
        RaycastHit hit;
        for (int i = 0; i <= _resolution; i++, _vertexIndex++)
        {
            // convert current angle to vector3
            _angleRadians = angle * (Mathf.PI / 180f);
            _angleVector = new Vector3(Mathf.Cos(_angleRadians), Mathf.Sin(_angleRadians));

            // check collision of cone
            float vertextDistance = _viewDistance;
            if(Physics.Raycast(transform.position, _angleVector, out hit, _viewDistance, _terrainMask))
            vertextDistance = hit.distance;


            // set vertices of polygon
            Vector3 vertex = _origin + _angleVector * vertextDistance;
            _vertices[_vertexIndex] = vertex;

            // stencil cone
            // check collision of cone
            float stencilVertextDistance = _viewDistance;

            // set vertices of polygon
            Vector3 stencilVertex = _origin + _angleVector * stencilVertextDistance;
            _stencilVertices[_vertexIndex] = stencilVertex;

            // add vertices of polygon to triangles array
            if (i > 0)
            {
                _triangles[_triangleIndex] = 0;
                _triangles[_triangleIndex + 1] = _vertexIndex - 1;
                _triangles[_triangleIndex + 2] = _vertexIndex;

                _stencilTriangles[_triangleIndex] = 0;
                _stencilTriangles[_triangleIndex + 1] = _vertexIndex - 1;
                _stencilTriangles[_triangleIndex + 2] = _vertexIndex;

                _triangleIndex += 3;
            }

            // increase angle clockwise
            angle -= _angleIncrease;
        }

        // set values to mesh to update cone
        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();

        _stencilMesh.vertices = _stencilVertices;
        _stencilMesh.uv = _stencilUv;
        _stencilMesh.triangles = _stencilTriangles;
        _stencilMesh.RecalculateNormals();

        // Check to scan collision
        if (!_scanWaiting) StartCoroutine(CollisionScan());

    }
    private void RepeatDrawFog()
    {
        DrawFogOfWar(_aimAngle + (_fov / 2f));
    }
    private void DrawFogOfWar(float angle)
    {
        Vector3[] vertices = new Vector3[3];
        int vertexIndex = 1;


        float vertexDistance = 0;

        float rayAngle = angle;

        RaycastHit hit;
        //calculate distance
        for (int i = 0; i <= _resolution; i++, _vertexIndex++)
        {
            // convert current angle to vector3
            float angleRadians = rayAngle * (Mathf.PI / 180f);
            Vector3 angleVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            if (Physics.Raycast(transform.position, _angleVector, out hit, _viewDistance, _terrainMask))
            {
                
                if(hit.distance > vertexDistance)
                    vertexDistance = hit.distance;


            }
            else
            {
                vertexDistance = _viewDistance;
                break;
            }


            // set vertices of polygon


            // increase angle clockwise
            rayAngle -= _angleIncrease;
        }








        vertices[0] = _origin;
        for (int i = 0; i <= 1; i++, vertexIndex++)
        {
            // convert current angle to vector3
            float angleRadians = angle * (Mathf.PI / 180f);
            Vector3 angleVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            // set vertices of polygon
            Vector3 vertex = _origin + angleVector * vertexDistance;
            vertices[vertexIndex] = vertex;

            // increase angle clockwise
            angle -= _fov;
        }

        transform.TransformPoints(vertices);

        FogOfWar.Instance.MakeTriangle(vertices[0], vertices[1], vertices[2], _fogAlpha);
    }

    public IEnumerator BlipGhostEffect(GameObject blip)
    {
        yield return new WaitForSeconds(1f / _sampleRate);
        blip.SetActive(false);
    }

    public IEnumerator CollisionScan()
    {
        _scanWaiting = true;
        // fire standarized raycasts to scan for collision
        for (int i = 0; i < _rayResolution; i++)
        {
            // check if ray angle is within view cone
            float rayAngle = (360f / _rayResolution) * i;

            if (Mathf.Abs(rayAngle - _aimAngle) < _fov / 2 || Mathf.Abs(rayAngle - _aimAngle) > 360 - _fov / 2)
            {
                // convert ray angle to vector
                _rayRadians = rayAngle * (Mathf.PI / 180f);
                _rayVector = new Vector3(Mathf.Cos(_rayRadians), Mathf.Sin(_rayRadians));
                // fire ray

                /* old 2D raycast system
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _rayVector, _viewDistance);
                if (raycastHit2D.collider != null)
                {
                    // draw blip on hit
                    _rayCollisions[i] = Instantiate(data.blip, raycastHit2D.point, Quaternion.identity);
                }*/

                RaycastHit hit;
                if (Physics.Raycast(transform.position, _rayVector, out hit, _viewDistance, _collisionMask))
                {
                    // draw blip on hit
                    _rayCollisions[i] = GetBlip(new Vector3(hit.point.x, hit.point.y, 3));
                    // check if enemy was hit
                    if (hit.collider.gameObject.layer == 8)
                        _rayCollisions[i].material = data.enemyColor;
                    else
                        _rayCollisions[i].material = data.defaultColor;
                    _rayCollisions[i].gameObject.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(1f/_sampleRate);
        _scanWaiting = false;
    }

    public Vector3 ConvertAimCoordinate(Vector2 aimInput)
    {
        Vector3 screenPoint = aimInput;
        screenPoint.z = Mathf.Abs(transform.position.z - camera2D.transform.position.z);
        Vector3 mainPoint = camera2D.ScreenToWorldPoint(screenPoint);


        //Debug.Log(mainPoint + ", player pos screen: " + camera2D.WorldToScreenPoint(transform.position) + ", screen size: " + camera2D.orthographicSize);
        return mainPoint;
    }
    private MeshRenderer GetBlip(Vector3 position)
    {
        for (int i = 0; i < _numBlips; i++)
        {
            if (!_blips[i].gameObject.activeInHierarchy)
            {
                _blips[i].transform.position = position;
                return _blips[i];
            }
        }
        return null;
    }
    private void OnDrawGizmosSelected()
    {
        float angle = _aimAngle + (_fov / 2f);


        Vector3[] vertices = new Vector3[3];
        int vertexIndex = 1;

        vertices[0] = _origin;
        for (int i = 0; i <= 1; i++, vertexIndex++)
        {
            // convert current angle to vector3
            float angleRadians = angle * (Mathf.PI / 180f);
            Vector3 angleVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians));

            // set vertices of polygon
            Vector3 vertex = _origin + angleVector * _viewDistance;
            vertices[vertexIndex] = vertex;

            // increase angle clockwise
            angle -= _fov;
        }

        transform.TransformPoints(vertices);
        for(int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 1f);
        }
    }

    private void UpdateConeMat()
    {
        Material[] materials = new Material[1];
        materials[0] = data.radarColor;
        _renderer.SetMaterials(materials.ToList());
    }
}
