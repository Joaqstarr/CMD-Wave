using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighPressureWarningZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSub"))
            if (!HighPressureRoom.HighPressureEquipped)
            {
                CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Warning: dangerous pressure levels detected nearby."), false);
            }
    }
}
