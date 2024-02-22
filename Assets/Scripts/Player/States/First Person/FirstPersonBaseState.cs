using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FirstPersonBaseState
{
    public abstract void OnEnterState(FirstPersonPlayerManager player);

    public abstract void OnExitState(FirstPersonPlayerManager player);

    public abstract void OnUpdateState(FirstPersonPlayerManager player);

}
