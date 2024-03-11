using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.Animations;

public class ScanDartObject : AbilityArchetype
{
    //public ScanDart _data;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private ParentConstraint _parent;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float faceAngle = transform.rotation.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 angleVector = new Vector3(Mathf.Cos(faceAngle), Mathf.Sin(faceAngle));
        //_rb.AddRelativeForce(angleVector * _data.launchForce, ForceMode.Force); 
    }

    private void OnTriggerEnter(Collider collision)
    {
        /*if ((_data.collideWith & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log(collision.gameObject);
            _rb.velocity = Vector3.zero;
            transform.SetParent(collision.gameObject.transform);
            _collider.enabled = false;
        }*/
    }
}
