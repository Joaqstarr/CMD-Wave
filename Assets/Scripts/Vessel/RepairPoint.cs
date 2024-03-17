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
    [SerializeField]
    private float _maxDist = 6f;
    public bool CheckInteractable(float distance)
    {
        if(distance > _maxDist)
            return false;
        return IsDamaged;
    }

    public void OnInteracted(PlayerControls playerInteracted)
    {
        Repair();
    }
    public void Repair()
    {
        VesselRoomHandler.Instance.UpdateMapColors();
        IsDamaged = false;
        _ParticleSystem.Stop();
        _projector.enabled = false;
        _audioSource.volume = 0;

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
