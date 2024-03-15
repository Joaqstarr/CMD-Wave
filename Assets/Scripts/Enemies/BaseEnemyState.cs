using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyState
{
    public abstract void OnEnterState(BaseEnemyManager enemy);

    public abstract void OnExitState(BaseEnemyManager enemy);

    public abstract void OnUpdateState(BaseEnemyManager enemy);

    public abstract void OnFixedUpdateState(BaseEnemyManager enemy);
}
