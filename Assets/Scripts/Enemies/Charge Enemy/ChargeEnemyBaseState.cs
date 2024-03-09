using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargeEnemyBaseState
{
    public abstract void OnEnterState(ChargeEnemyManager enemy);

    public abstract void OnExitState(ChargeEnemyManager enemy);

    public abstract void OnUpdateState(ChargeEnemyManager enemy);

    public abstract void OnFixedUpdateState(ChargeEnemyManager enemy);
}
