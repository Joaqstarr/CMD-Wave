using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDeadState : BaseEnemyState
{
    private FinalBossManager manager;
    private float _deathDrag = 5f;
    public override void OnEnterState(BaseEnemyManager enemy)
    {
        Debug.Log("final boss dead");
        if (manager == null)
            manager = (FinalBossManager)enemy;

        manager._source.pitch *= 0.7f;
        manager._source.Play();

        manager.Pathfinder.maxSpeed *= 0.5f;
        manager.Pathfinder.maxAcceleration *= 0.5f;
        manager.Pathfinder.transform.DOKill();
        manager.Pathfinder.transform.DOMove(manager.Pathfinder.transform.position - (manager.Pathfinder.transform.position - manager.Target.position).normalized * manager._data.knockbackValue, 3f).SetEase(Ease.OutSine);
        manager.DestinationSetter.transform.DORotate((manager.DestinationSetter.target.position - manager.transform.position) + Vector3.right, 6f);
        enemy.Pathfinder.canMove = false;

        //manager.StartCoroutine(DeathAnim());
    }

    public override void OnExitState(BaseEnemyManager enemy)
    {
    }

    public override void OnFixedUpdateState(BaseEnemyManager enemy)
    {
    }

    public override void OnUpdateState(BaseEnemyManager enemy)
    {
    }

}
