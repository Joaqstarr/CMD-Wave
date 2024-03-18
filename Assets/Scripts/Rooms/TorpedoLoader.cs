using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLoader : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TorpedoData _torpedo;

    [SerializeField]
    private GameObject _player;
    private Animator _animator;
    [SerializeField]
    private float _maxDist = 5f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public bool CheckInteractable(float distance)
    {
        if(_maxDist < distance)
        {
            return false;
        }
        return _torpedo.CurrentAmmo < _torpedo.MaxAmmo;
    }

    public string GetInteractableLabel()
    {
        return "Press E to reload Torpedos";
    }

    public void OnInteracted(PlayerControls playerInteracted)
    {
        
        _animator.SetInteger("Reload", _torpedo.Reload(_player));
    }


}
