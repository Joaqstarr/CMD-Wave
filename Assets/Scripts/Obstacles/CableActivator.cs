using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CableActivator : MonoBehaviour, IDataPersistance
{
    [SerializeField]
    private UnityEvent _onActivate;
    [SerializeField]
    //private UnityEvent _onDeactivate;
    private Collider _collider;

    private bool _open = false;

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
            _open = true;
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

    public void SaveData(ref SaveData data)
    {
        if (data._doorValues.ContainsKey(transform.position))
        {
            data._doorValues[transform.position] =  _open;

        }
        else
        {
            data._doorValues.Add(transform.position, _open);
        }
    }

    public void LoadData(SaveData data)
    {
        if (data._doorValues.ContainsKey(transform.position))
            _open = data._doorValues[transform.position];

        if (_open)
        {
            _onActivate.Invoke();

        }
    }
}
