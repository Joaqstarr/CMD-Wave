using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScreenDamageIndicator : MonoBehaviour
{

    private RawImage _screenImage;
    private Material _screenMaterial;
    [SerializeField]
    private float _startGlitchiness = 0.015f;
    [SerializeField]
    private float _endGlitchiness;
    [SerializeField]
    private TweenData _enterTween;
    [SerializeField]
    private TweenData _exitTween;
    float _strength;

    // Start is called before the first frame update
    void Start()
    {
        _screenImage = GetComponent<RawImage>();
        _screenMaterial = _screenImage.material;
        _screenMaterial.SetFloat("_GlitchAmount", _startGlitchiness);
    }

    private void OnEnable()
    {
        PlayerSubHealth.OnHitDel += Damage;
    }
    private void OnDisable()
    {
        PlayerSubHealth.OnHitDel -= Damage;
    }

    private void Damage(float strength)
    {
        _strength = strength;
        DOVirtual.Float(_startGlitchiness , _endGlitchiness * _strength, _enterTween.Duration, (x) => { _screenMaterial.SetFloat("_GlitchAmount", x); }).SetEase(_enterTween.Ease).OnComplete(() =>
        {
            DOVirtual.Float(_endGlitchiness * _strength, _startGlitchiness, _exitTween.Duration, (x) => { _screenMaterial.SetFloat("_GlitchAmount", x); }).SetEase(_exitTween.Ease);

        });
    }

}
