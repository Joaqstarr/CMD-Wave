using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public interface IInteractable
{
    public void OnInteracted(PlayerControls playerInteracted);

    public bool CheckInteractable(float distance);

    public string GetInteractableLabel();
}
