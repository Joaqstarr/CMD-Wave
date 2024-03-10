using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanDartObject : MonoBehaviour
{
    private Rigidbody _rb;
    private CapsuleCollider _collider;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rb.velocity = Vector3.zero;
        transform.parent = collision.gameObject.transform;
        _collider.enabled = false;
    }
}
