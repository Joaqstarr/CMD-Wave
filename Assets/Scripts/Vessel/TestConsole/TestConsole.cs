using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConsole : MonoBehaviour, IInteractable
{
    public void OnInteracted()
    {
        GetComponentInChildren<CinemachineVirtualCamera>().Priority = 11;
    }


}
