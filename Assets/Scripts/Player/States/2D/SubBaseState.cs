using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SubBaseState
{
    public abstract void OnEnterState(PlayerSubManager player);

    public abstract void OnExitState(PlayerSubManager player);

    public abstract void OnUpdateState(PlayerSubManager player);

    public abstract void OnFixedUpdateState(PlayerSubManager player);

}
