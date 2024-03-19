using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DashTerrain : MonoBehaviour, IDataPersistance
{
    [SerializeField]
    private float _delayDestroyTime= 3;
    Collider _collider;
    private bool _open = false;  
    MeshRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && DashData.IsMaxDashing)
        {
            Activate();
            /*GetComponent<Collider>().enabled = false;
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                StartCoroutine(DelayDestroy());
            }
            else*/

            //Destroy(gameObject);
        }
            
    }



    public void SaveData(ref SaveData data)
    {
        if (data._doorValues.ContainsKey(transform.position))
        {
            data._doorValues[transform.position] = _open;

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
            Activate();
        }
        else
        {
            Spawn();
        }
    }
    private void Activate()
    {
        _open = true;
        _renderer.enabled = false;
        _collider.enabled = false;
    }
    private void Spawn()
    {
        _open = false;
        _renderer.enabled = true;
        _collider.enabled = true;
    }
}
