using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class CurrentBehavior : MonoBehaviour
{
    [SerializeField]
    private Vector3[] _pushLocations;
    [SerializeField]
    private float _pushStrength = 5f;
    [SerializeField]
    Vector3 _particleDirection;

    private ParticleSystem _particleSystem;
    [SerializeField]
    private bool _alwaysHide = false;



    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        //_boxCollider = GetComponent<BoxCollider>();
       // _particleSystemForceField = GetComponent<ParticleSystemForceField>();

        _particleDirection.Normalize();
        //_particleSystemForceField.directionX = _particleDirection.x * _pushStrength;
        //_particleSystemForceField.directionY = _particleDirection.y * _pushStrength;
        //_particleSystemForceField.directionZ = _particleDirection.z * _pushStrength;


    }

    // Update is called once per frame
    void Update()
    {
        if(_particleSystem.isEmitting && !CurrentScan.CurrentScanEquipped || _alwaysHide)
        {
            _particleSystem.Stop();
            _particleSystem.Clear();

        }
        if (!_particleSystem.isEmitting && CurrentScan.CurrentScanEquipped)
        {
            _particleSystem.Play();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        for(int i = 0; i < _pushLocations.Length; i++)
        {
            Gizmos.DrawWireSphere(transform.TransformPoint(_pushLocations[i]), 0.5f);
        }

        Gizmos.color = Color.red;

        Ray ray = new Ray(transform.position, _particleDirection);
        Gizmos.DrawRay(ray);
    }
    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb= other.GetComponent<Rigidbody>();
        if(rb != null)
        {
            Vector3 positionToPushTowards = GetNearestPosition(other.transform.position);

            Vector3 direction = positionToPushTowards - other.transform.position;

            direction.Normalize();

            rb.AddForce(direction * _pushStrength, ForceMode.Force);


        }
    }

    private Vector3 GetNearestPosition(Vector3 position)
    {
        Vector3 closestPosition = transform.TransformPoint(_pushLocations[0]);
        for(int i = 1; i < _pushLocations.Length; i++)
        {
            if(Vector3.Distance(position, transform.TransformPoint(_pushLocations[i])) < Vector3.Distance(position, closestPosition))
                closestPosition = transform.TransformPoint(_pushLocations[i]);
        }

        return closestPosition;
    }
}
