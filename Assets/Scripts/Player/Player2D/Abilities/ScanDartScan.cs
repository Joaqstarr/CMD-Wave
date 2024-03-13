using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class ScanDartScan : MonoBehaviour
{

    public ScanDartData data;

    private Mesh _mesh; // mesh for cone polygons
    private Vector3 _origin; // starting point of cone - should be on player
    private float _fov; // how wide cone is in degrees
    private int _resolution; // how many polygons make up the cone mesh - smoothness
    private float _viewDistance; // how far out the cone goes
    private int _rayResolution; // resolution of rays cast for scanning collision
    private float _blipGhostTime; // how long blips stay visible after out of vision
    private int _sampleRate; // how many times a second the cone scans for collision - UNUSED
    private LayerMask _collisionMask; // layer mask for raycast scan collisions

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
    private GameObject[] _rayCollisions;
    private GameObject _pooledBlip;
    private List<GameObject> _blips;
    private int _numBlips;
    [SerializeField]
    private float _fogAlpha = 0.3f;
    [SerializeField]
    private float _fogRefreshRate = 0.5f;
    private Vector2 _lastFogPosition;
    [SerializeField]
    private float _minimumFogDistance = 8;

    private void Start()
    {

        // variable assignments
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        //GetComponent<MeshCollider>().sharedMesh = _mesh;

        _origin = transform.localPosition;
        _fov = data.fov;
        _resolution = data.resolution;
        _viewDistance = data.viewDistance;
        _rayResolution = data.rayResolution;
        //_blipGhostTime = data.blipGhostTime;
        _sampleRate = data.sampleRate;
        _collisionMask = data.collisionMask;

        _scanWaiting = false;

        _vertices = new Vector3[_resolution + 2];
        _uv = new Vector2[_vertices.Length];
        _triangles = new int[_resolution * 3];
        _rayCollisions = new GameObject[_rayResolution];

        _vertices[0] = _origin;

        // Blip object pool
        _blips = SubViewCone._blips;
        _numBlips = _blips.Count;

        DrawViewCone();

    }

    private void OnEnable()
    {
        //InvokeRepeating("MakeFogHole", _fogRefreshRate, _fogRefreshRate);
    }

    private void Update()
    {
        MakeFogHole();

        if (!_scanWaiting) StartCoroutine(CollisionScan());
    }

    public void DrawViewCone()
    {

        //render mesh
        _triangleIndex = 0;
        _vertexIndex = 1;
        for (int i = 0; i <= _resolution; i++, _vertexIndex++)
        {
            // set angle
            _angleRadians = (360f / _resolution) * (_resolution - i) * (Mathf.PI/180f);
            _angleVector = new Vector3(Mathf.Cos(_angleRadians), Mathf.Sin(_angleRadians));

            // set vertices of polygon
            Vector3 vertex = _origin + _angleVector * _viewDistance;
            _vertices[_vertexIndex] = vertex;

            // add vertices of polygon to triangles array
            if (i > 0)
            {
                _triangles[_triangleIndex] = 0;
                _triangles[_triangleIndex + 1] = _vertexIndex - 1;
                _triangles[_triangleIndex + 2] = _vertexIndex;

                _triangleIndex += 3;
            }
        }

        // set values to mesh to update cone
        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

        // Check to scan collision
        if (!_scanWaiting) StartCoroutine(CollisionScan());

    }

    public void DeleteBlips()
    {
        // delete any blips from last frame
        if (_rayCollisions.Count() > 0)
            for (int i = 0; i < _rayCollisions.Count(); i++)
            {
                if (_rayCollisions[i] != null)
                {
                    Debug.Log("gone");
                    //StartCoroutine(BlipGhostEffect(_rayCollisions[i]));
                    _rayCollisions[i].SetActive(false);
                    _rayCollisions[i] = null;
                }
            }
    }

    public IEnumerator BlipGhostEffect(GameObject blip)
    {
        yield return new WaitForSeconds(1f / _sampleRate);
        blip.SetActive(false);
    }

    public IEnumerator CollisionScan()
    {
        DeleteBlips();

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
                    Debug.Log("blip");
                    // draw blip on hit
                    _rayCollisions[i] = GetBlip(hit.point);
                    // check if enemy was hit
                    if (hit.collider.gameObject.layer == 8)
                        _rayCollisions[i].GetComponent<MeshRenderer>().material = data.enemyColor;
                    else
                        _rayCollisions[i].GetComponent<MeshRenderer>().material = data.defaultColor;
                    _rayCollisions[i].SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(1f / _sampleRate);
        _scanWaiting = false;
    }

    private GameObject GetBlip(Vector3 position)
    {
        for (int i = 0; i < _numBlips; i++)
        {
            if (!_blips[i].activeInHierarchy)
            {
                _blips[i].transform.position = position;
                return _blips[i];
            }
        }
        return null;
    }
    private void MakeFogHole()
    {
        if (Vector3.Distance(transform.position, _lastFogPosition) <= _minimumFogDistance) return;
        _lastFogPosition = transform.position;
        FogOfWar.Instance.MakeHole(transform.position, _viewDistance, _fogAlpha);
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
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 1f);
        }
    }
}
