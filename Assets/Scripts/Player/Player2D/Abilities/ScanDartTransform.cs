using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Animations;

public class ScanDartTransform : MonoBehaviour
{
    public ScanDartData _data;
    public GameObject _dartVisuals;
    public GameObject _scanArea;
    private GameObject _parent;
    private Rigidbody _rb;
    private CapsuleCollider _collider;

    [HideInInspector]
    public bool _collided = false;



    // Start is called before the first frame update
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        _parent = transform.parent.gameObject;
        _rb.isKinematic = false;

    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        _dartVisuals.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        /*float faceAngle = transform.rotation.eulerAngles.z * (Mathf.PI / 180f);
        Vector3 angleVector = new Vector3(Mathf.Cos(faceAngle), Mathf.Sin(faceAngle)); 
        _rb.AddRelativeForce(angleVector * _data.launchForce, ForceMode.Impulse); */
    }

    public void ResetDart()
    {
        _scanArea.GetComponent<ScanDartScan>().DeleteBlips();
        Debug.Log("deleted");
        _rb.velocity = Vector3.zero;
        transform.position = _parent.transform.position;
        if (transform.parent != _parent.transform)
            transform.parent = _parent.transform;
        _collider.enabled = true;
        _collided = false;

        _rb.isKinematic = false;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((_data.collideWith & (1 << collision.gameObject.layer)) != 0)
        {
            Debug.Log(collision.gameObject);
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            transform.SetParent(collision.transform);
            _collider.enabled = false;
        }
    }
}
