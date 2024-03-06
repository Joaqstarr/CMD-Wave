using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyBaseState
{
    public abstract void OnEnterState(ShyEnemyManager enemy);

    public abstract void OnExitState(ShyEnemyManager enemy);

    public abstract void OnUpdateState(ShyEnemyManager enemy);

    public abstract void OnFixedUpdateState(ShyEnemyManager enemy);
}
