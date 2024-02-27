using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UIElements;

public class SubViewCone : MonoBehaviour
{
    public PlayerSubData data;

    private Mesh _mesh; // mesh for cone polygons
    private Vector3 _origin; // starting point of cone - should be on player
    private float _fov; // how wide cone is in degrees
    private int _resolution; // how many polygons make up the cone mesh - smoothness
    private float _viewDistance; // how far out the cone goes

    private float _angle;
    private float _angleIncrease;
    private float _angleRadians;
    private Vector3 _angleVector;
    private int _vertexIndex;
    private int _triangleIndex;

    private Vector3[] _vertices;
    private Vector2[] _uv;
    private int[] _triangles;

    private void Start()
    {
        // variable assignments
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        _origin = transform.localPosition;
        _fov = data.fov;
        _resolution = data.resolution;
        _viewDistance = data.viewDistance;

        _angle = 0f;
        _angleIncrease = _fov / _resolution;

        _vertices = new Vector3[_resolution + 2];
        _uv = new Vector2[_vertices.Length];
        _triangles = new int[_resolution * 3];

        _vertices[0] = _origin;
    }


    public void DrawViewCone(Vector3 aimPos)
    {
        // convert mouse position to angle
        aimPos = Camera.main.ScreenToWorldPoint(aimPos);
        aimPos = (aimPos - transform.position).normalized;
        aimPos.z = 0;
        float aimAngle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg;
        if (aimAngle < 0) aimAngle += 360;
        
        _angle = aimAngle + (_fov/2f);
         
        _triangleIndex = 0;
        _vertexIndex = 1;
        for (int i = 0; i <= _resolution; i++, _vertexIndex++)
        {
            // convert current angle to vector3
            _angleRadians = _angle * (Mathf.PI / 180f);
            _angleVector = new Vector3(Mathf.Cos(_angleRadians), Mathf.Sin(_angleRadians));

            // set vertices of polygon
            Vector3 vertex = _origin + _angleVector * _viewDistance;
            _vertices[_vertexIndex] = vertex;

            
            // raycast to check scan for collision
            if (i % 5 == 0)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, _angleVector, _viewDistance);
                if (raycastHit2D.collider != null)
                {
                    Debug.Log("hit");
                    Instantiate(data.blip, raycastHit2D.point, Quaternion.identity);
                }
            }


            // add vertices of polygon to triangles array
            if (i > 0)
            {
                _triangles[_triangleIndex] = 0;
                _triangles[_triangleIndex + 1] = _vertexIndex - 1;
                _triangles[_triangleIndex + 2] = _vertexIndex;

                _triangleIndex += 3;
            }

            // increase angle clockwise
            _angle -= _angleIncrease;
        }

        // set values to mesh to update cone
        _mesh.vertices = _vertices;
        _mesh.uv = _uv;
        _mesh.triangles = _triangles;

    }
}
