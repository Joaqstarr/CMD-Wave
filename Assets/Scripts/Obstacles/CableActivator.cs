using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CableActivator : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onActivate;
    [SerializeField]
    //private UnityEvent _onDeactivate;
    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ScanDart"))
        {
            _onActivate.Invoke();
        }

    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ScanDart"))
        {
            _onDeactivate.Invoke();
        }

    }*/

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
