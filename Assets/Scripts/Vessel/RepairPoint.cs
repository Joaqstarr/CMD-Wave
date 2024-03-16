using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class RepairPoint : MonoBehaviour, IInteractable
{

    private AudioSource _audioSource;
    private ParticleSystem _ParticleSystem;
    private DecalProjector _projector;

    public bool IsDamaged = false;
    public bool CheckInteractable(float distance)
    {
        return true;
    }

    public void OnInteracted(PlayerControls playerInteracted)
    {
        _audioSource.volume =0;
        _projector.enabled = false;
        _ParticleSystem.Stop();
        IsDamaged = false;
    }

    private void Start()
    {
        _projector = GetComponentInChildren<DecalProjector>();
        _audioSource = GetComponent<AudioSource>();
        _ParticleSystem = GetComponentInChildren<ParticleSystem>();
        _audioSource.volume = 0;
        _projector.enabled = false;
        _ParticleSystem.Stop();
        IsDamaged = false;

    }
    public void Damage()
    {
        IsDamaged = true;
        _audioSource.volume = 1;
        _projector.enabled = true;
        _ParticleSystem.Play();
    }

    public string GetInteractableLabel()
    {
        return "Press E to Repair Damage";
    }
}
