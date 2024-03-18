using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class ProbeObject : MonoBehaviour
{
    [SerializeField]
    private ProbeData _data;
    public ProbeControls _probeControls;
    public Rigidbody _rb;
    public CinemachineVirtualCamera _probeVirtualCamera;
    public GameObject _player;
    public ProbeScan _probeScan;
    public Transform _camFollow;
    public Collider _detonationArea;
    public ParticleSystem _detonationFX;

    private Transform _parent;

    [HideInInspector]
    public bool _detonating;

    [SerializeField]
    private AudioClip[] _hitClips;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        gameObject.SetActive(false);
        _parent = transform.parent;
    }

    private void OnDisable()
    {
        _camFollow.position = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateCamFollow();
    }

    private void FixedUpdate()
    {
        Move(ProbeControls.Instance.MoveInput);

        _probeScan.DrawViewCone(ProbeControls.Instance.AimInput);
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

    private void UpdateCamFollow()
    {


        _camFollow.localPosition = Vector3.ClampMagnitude((Vector3.zero + (_probeScan.ConvertAimCoordinate(_probeControls.AimInput) - transform.position)), _data.cameraLookAhead);
    }
    public void Detonate()
    {
        _detonating = true;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        _detonationArea.enabled = true;
        _probeScan.gameObject.SetActive(false);
        transform.Find("ProbeVisual").gameObject.SetActive(false);
        _detonationFX.Play();

        yield return new WaitForSeconds(_data.detonationReturnTime);

        _probeVirtualCamera.Priority = 0;
        Reparent();
        _probeScan.gameObject.SetActive(true);
        transform.Find("ProbeVisual").gameObject.SetActive(true);
        gameObject.SetActive(false);
        _detonationArea.enabled = false;

        _data._isControlling = false;
        _data._probeLoaded = false;
        ProbeData.ProbeDeployed = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            _data.UseAbility(_player, this.gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (_detonating)
        {
            IHittable hitInterface = other.gameObject.GetComponent<IHittable>();
            if (hitInterface != null)
            {
                hitInterface.Hit(_data.detonationDamage, this.gameObject);
                Debug.Log(other.gameObject + " hit!");
            }
        }
    }

}
