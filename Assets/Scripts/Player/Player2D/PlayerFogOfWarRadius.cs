using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFogOfWarRadius : MonoBehaviour
{
    [SerializeField]
    private float _repeatWait = 0.1f;
    [SerializeField]
    private float _radius = 10f;
    [SerializeField]
    private float _alpha = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeFogHole", _repeatWait, _repeatWait);
    }
    
    private void MakeFogHole()
    {
        FogOfWar.Instance.MakeHole(transform.position, _radius, _alpha);
    }
}
