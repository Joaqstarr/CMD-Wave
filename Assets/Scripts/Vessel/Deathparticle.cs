using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathparticle : MonoBehaviour
{

    private ParticleSystem _system;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _system = GetComponent<ParticleSystem>();
    }

    private void OnDeath(float str)
    {
        _system.Play();
        _audioSource.Play();
    }

    private void OnEnable()
    {
        PlayerSubHealth.OnDeathDel += OnDeath;
    }
    private void OnDisable()
    {
        PlayerSubHealth.OnDeathDel -= OnDeath;

    }
}
