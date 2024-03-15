using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConsole : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerControls _subPlayer;
    [SerializeField] private float _range = 2;
    
    public bool CheckInteractable(float distance)
    {
        return distance < _range;
    }

    public void OnInteracted(PlayerControls playerInteracted)
    {
        //GetComponentInChildren<CinemachineVirtualCamera>().Priority = 11;
        playerInteracted.Possess(_subPlayer);
    }


}
