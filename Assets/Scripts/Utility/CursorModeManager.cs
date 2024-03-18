using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorModeManager : MonoBehaviour
{


    void Update()
    {
        if(PauseManager.Instance != null)
            if (PauseManager.Instance.IsPause)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }

        if (FirstPersonPlayerControls.Instance != null)
            if (FirstPersonPlayerControls.Instance.IsPosessing)
            {
                Cursor.lockState = CursorLockMode.Confined;
                return;
            }

        Cursor.lockState = CursorLockMode.Locked;

    }
}
