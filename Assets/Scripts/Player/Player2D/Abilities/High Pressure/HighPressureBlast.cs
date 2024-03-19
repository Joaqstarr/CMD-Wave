using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class HighPressureBlast : MonoBehaviour
{
    public HighPressureData _data;
    public ParticleSystem _particles;
    public SphereCollider _collider;

    private  ParticleSystem.MainModule _particlesMain;
    private ParticleSystem.ShapeModule _particlesShape;

    private bool _blasting = false;
    [HideInInspector]
    public bool _hasBlast = true;

    private Transform _parent;
    private float _runTime;

    private void Awake()
    {
        _hasBlast = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _parent = GameObject.Find("HighPressureHolder").transform;
        transform.position = _parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_blasting && _hasBlast && _runTime < _data.blastDuration)
        {
            if (!_particles.isPlaying)
            {
                transform.position = GameObject.Find("SubPlayer").transform.position;
                _particles.Play();
            }


            _runTime += Time.deltaTime;
            _collider.radius = Mathf.Lerp(0, _data.blastRadius, _runTime);
        }
        else if (_runTime >= _data.blastDuration)
        {
            _runTime = 0;
            StartCoroutine(Deactivate());
            _collider.radius = 0.001f;
            _collider.enabled = false;
        }

        if (!_particles.isPlaying)
            _blasting = false;
        
    }

    public void StartBlast()
    {
        _blasting = true;
        _runTime = 0;
        _collider.enabled = true;

    }
    public void RechargeBlast()
    {
        transform.position = GameObject.Find("SubPlayer").transform.position;
        //_hasBlast = true;
    }
    private IEnumerator Deactivate()
    {
        //_hasBlast = false;
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        IKnockbackable knockbackInterface = collision.gameObject.GetComponent<IKnockbackable>();
        if (knockbackInterface != null)
            knockbackInterface.Knockback(_data.knockback, _data.stunDuration, transform.position);
            StartCoroutine(ApplyKnockback(collision, knockbackInterface));
    }

    private IEnumerator ApplyKnockback(Collider collision, IKnockbackable knockbackInterface)
    {
        yield return new WaitForFixedUpdate();
        knockbackInterface.Knockback(_data.knockback, _data.stunDuration, transform.position);
    }
}
