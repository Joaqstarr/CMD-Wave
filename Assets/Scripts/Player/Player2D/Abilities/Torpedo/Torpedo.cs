using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public TorpedoData _data;

    private Rigidbody _rb;
    private AudioSource _explodeSource;
    [SerializeField] private AudioClip _explodeSound;
    // Start is called before the first frame update
    void Start()
    {
        _explodeSource = transform.parent.GetComponent<AudioSource>();
        if (_explodeSource == null)
        {
            _explodeSource = transform.parent.gameObject.AddComponent<AudioSource>();
            _explodeSource.playOnAwake = false;
            _explodeSource.loop = false;
            _explodeSource.outputAudioMixerGroup = GetComponent<AudioSource>().outputAudioMixerGroup;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _explodeSource.PlayOneShot(_explodeSound);
        if ((_data.collideWith & (1 << collision.gameObject.layer)) != 0)
        {
            
            Debug.Log(collision.gameObject);
            IHittable hitInterface = collision.gameObject.GetComponentInParent<IHittable>();
            if (hitInterface != null)
            {
                hitInterface.Hit(_data.damage, this.gameObject);
            }
            this.gameObject.SetActive(false);
        }
    }
}
