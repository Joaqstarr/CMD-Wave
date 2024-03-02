using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private PlayerSubManager _player;

    private float _damage = 15;
    private float _stunDuration = 1;
    private float _knockbackValue = 30;
    private Vector3 _knockbackVector;

    private Vector3 _lastPos;
    private Vector3 _velocity;

    private Rigidbody _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerSub").GetComponent<PlayerSubManager>();
        _rb = GetComponent<Rigidbody>();

        _lastPos = transform.position;
        _knockbackVector = Vector3.zero;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        // calculate velocity
        _velocity = (transform.position - _lastPos) / Time.deltaTime;
        _velocity.Normalize();
        
        // calculate knockback based on velocity vector
        if (_velocity.magnitude > 0)
        {
            float xForce = _knockbackValue * Mathf.Abs(_velocity.x / _velocity.magnitude) * Mathf.Sign(_velocity.x);
            float yForce = _knockbackValue * Mathf.Abs(_velocity.y / _velocity.magnitude) * Mathf.Sign(_velocity.y);
            _knockbackVector = new Vector3(xForce, yForce, 0);
        }
        else
        {
            _knockbackVector = Vector3.zero;
        }

        _lastPos = transform.position;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub"))
        {
            Debug.Log(_knockbackVector);
            _player.Health.OnHit(_damage, _knockbackVector, _stunDuration);
        }
    }
}
