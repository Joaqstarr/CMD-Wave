using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCommand : CommandBase
{
    private bool _isFlying;
    [SerializeField]
    private PlayerControls _mapControls;
    [SerializeField]
    CinemachineVirtualCamera _mapVirtualCamera;

    public override string[] Execute(out CommandContext overrideContext, string arg = null)
    {

        overrideContext = null;
        if (_isFlying)
        {
            FirstPersonPlayerControls.Instance.UnPossess(_mapControls);
            _mapVirtualCamera.Priority = 0;
            _isFlying = false;
            return CommandLineManager.StringToArray("Exiting Map View");

        }
        else
        {
            FirstPersonPlayerControls.Instance.Possess(_mapControls, false);
            _isFlying = true;
            transform.position = PlayerSubControls.Instance.transform.position;

            _mapVirtualCamera.Priority = 11;
            return CommandLineManager.StringToArray("Entering Map View. Re-enter Command to exit.");

        }

    }
}
