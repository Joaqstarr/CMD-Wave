using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthLabel : MonoBehaviour
{
    private TMP_Text _label;
    private bool _critical = false;
    private RectTransform _rectTransform;
    [SerializeField]
    private TweenData _tweenData;
    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _label = GetComponent<TMP_Text>();
        InvokeRepeating("ShakeWhenCritical", _tweenData.Duration, _tweenData.Duration);
    }

    // Update is called once per frame
    void Update()
    {
        _label.text = PlayerSubHealth.Instance.Health.ToString();
        if(PlayerSubHealth.Instance.Health <= PlayerSubHealth.Instance.data.healthNoDrain)
        {
            _label.color = Color.red;
            _critical = true;
        }
        else
        {
            _critical = false;
            _label.color = Color.white;
        }
    }

    private void ShakeWhenCritical()
    {
        if (!_critical) return;
        _rectTransform.DOShakeAnchorPos(_tweenData.Duration, _tweenData.Strength, 10, 90, false, false);
    }
}
