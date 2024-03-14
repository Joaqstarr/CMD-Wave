using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanDartParent : MonoBehaviour
{
    public GameObject _childTransform;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void RecallTransform()
    {
        _childTransform.transform.parent = transform;
    }
}
