using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeLoader : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ProbeData _probe;
    [SerializeField]
    private float _maxDist = 5f;
    bool reloaded = true;
    [SerializeField]
    private TweenData _tween;
    [SerializeField]
    private Vector3 StartPos;
    [SerializeField]
    private Vector3 endPos;
    [SerializeField]
    private Transform _probeTransform;

    public bool CheckInteractable(float distance)
    {
        if (_maxDist < distance)
        {
            return false;
        }
        return !_probe._probeLoaded;

    }

    public string GetInteractableLabel()
    {
        return "Press E to Reload Probe";
    }

    public void OnInteracted(PlayerControls playerInteracted)
    {
        reloaded = true;
        _probe.ReloadProbe();
        _probeTransform.DOKill();
        _probeTransform.DOLocalMove(endPos, _tween.Duration).SetEase(_tween.Ease);

    }


    void Update()
    {
        if(reloaded && !_probe._probeLoaded)
        {
            reloaded = false;
            _probeTransform.DOKill();
            _probeTransform.DOLocalMove(StartPos, _tween.Duration).SetEase(_tween.Ease);
        }
    }
}
