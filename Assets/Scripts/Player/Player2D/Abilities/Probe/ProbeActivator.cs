using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProbeActivator : MonoBehaviour
{
    public UnityEvent _onActivate;
    public bool _destroyOnActivation;

    private bool _activated;
    // Start is called before the first frame update
    void Start()
    {
        _activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Probe"))
        {
            if (other.gameObject.GetComponent<ProbeObject>()._detonating)
            {
                if (!_activated)
                {
                    _onActivate.Invoke();

                    if (_destroyOnActivation)
                        this.gameObject.SetActive(false);
                }

                _activated = true;
            }
        }
    }
}
