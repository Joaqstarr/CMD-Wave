using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLight : MonoBehaviour
{
    private Light _emergenLight;
    private float _maxStrength;
    [SerializeField]
    private float _flashSpeed =5f;
    // Start is called before the first frame update
    void Start()
    {
        _emergenLight = GetComponent<Light>();
        _maxStrength = _emergenLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {

        if (!EnemyDetector.EnemyNearby)
        {
            _emergenLight.intensity = 0;
        }else
        _emergenLight.intensity = ((Mathf.Sin(Time.time * _flashSpeed)/2)+0.5f) * _maxStrength;
    }
}
