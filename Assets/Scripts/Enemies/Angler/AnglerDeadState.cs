using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerDeadState : BaseEnemyState
{
    private AnglerManager manager;

    public override void OnEnterState(BaseEnemyManager enemy)
    {

        if (manager == null)
            manager = (AnglerManager)enemy;

        manager.Rb.useGravity = true;
        manager.Rb.isKinematic = false;

    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
        manager.Rb.useGravity = false;
        manager.Rb.isKinematic = true;


    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
    }
}
