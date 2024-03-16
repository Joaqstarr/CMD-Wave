using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeObject : MonoBehaviour
{
    [SerializeField]
    private ProbeData _data;
    public ProbeControls _probeControls;
    public Rigidbody _rb;
    public CinemachineVirtualCamera _mapVirtualCamera;

    private Transform _parent;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        _parent = transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move(ProbeControls.Instance.MoveInput);
    }

    public void Reparent()
    {
        transform.parent = _parent;
        transform.position = _parent.position;
    }

    public void Move(Vector2 input)
    {
        // move force
        Vector2 target = new Vector2(input.x * _data.moveSpeed, input.y * _data.moveSpeed);

        float difX = target.x - _rb.velocity.x;
        float difY = target.y - _rb.velocity.y;

        float accelVal = (input.magnitude < _rb.velocity.magnitude) ? _data.acceleration : _data.decceleration;

        float forceX = Mathf.Pow(Mathf.Abs(difX) * accelVal, _data.velocityModifier) * Mathf.Sign(difX);
        float forceY = Mathf.Pow(Mathf.Abs(difY) * accelVal, _data.velocityModifier) * Mathf.Sign(difY);
        Vector2 netForce = new Vector2(forceX, forceY);

        _rb.AddForce(netForce);

        // friction
        if (ProbeControls.Instance.MoveInput.x > 0.01)
        {
            float frictionX = Mathf.Min(Mathf.Abs(_data.frictionModifier), Mathf.Abs(_rb.velocity.x));

            frictionX *= Mathf.Sign(_rb.velocity.x);

            float frictionY = Mathf.Min(Mathf.Abs(_data.frictionModifier), Mathf.Abs(_rb.velocity.y));

            frictionY *= Mathf.Sign(_rb.velocity.y);

            _rb.AddForce(new Vector2(-frictionX, -frictionY), ForceMode.Impulse);
        }

    }
}
