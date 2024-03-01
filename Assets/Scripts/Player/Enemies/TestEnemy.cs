using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    private PlayerSubManager _player;

    private float _damage = 15;
    private float _stunDuration = 1;
    private float _knockbackValue = 10;
    private Vector3 _knockbackVector;

    private Vector3 _lastPos;
    private Vector3 _velocity;

    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerSub").GetComponent<PlayerSubManager>();
        _rb = GetComponent<Rigidbody>();

        _lastPos = transform.position;
        _knockbackVector = new Vector3(_knockbackValue, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = (transform.position - _lastPos) / Time.deltaTime;
       _knockbackVector = new Vector3(_velocity.x, _velocity.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub"))
        {
            _player.Health.OnHit(_damage, _knockbackVector, _stunDuration);
            Debug.Log("enemy collision");
        }
    }
}
