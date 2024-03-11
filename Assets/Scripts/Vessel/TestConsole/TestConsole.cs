using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConsole : MonoBehaviour, IInteractable
{
    [SerializeField] PlayerControls _subPlayer;

    public void OnInteracted(PlayerControls playerInteracted)
    {
        //GetComponentInChildren<CinemachineVirtualCamera>().Priority = 11;
        playerInteracted.Possess(_subPlayer);
    }


}
