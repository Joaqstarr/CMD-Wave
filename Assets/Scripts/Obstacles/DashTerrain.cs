using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashTerrain : MonoBehaviour
{
    [SerializeField]
    private float _delayDestroyTime= 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && DashData.IsMaxDashing)
        {
            /*GetComponent<Collider>().enabled = false;
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(collision.gameObject.GetComponent<Rigidbody>().velocity, ForceMode.Impulse);
                StartCoroutine(DelayDestroy());
            }
            else*/

            Destroy(gameObject);
        }
            
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_delayDestroyTime);
        Destroy(gameObject);
    }
}
