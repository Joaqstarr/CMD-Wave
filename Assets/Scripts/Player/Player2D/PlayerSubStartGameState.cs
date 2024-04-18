using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubStartGameState : SubBaseState
{

    public override void OnEnterState(PlayerSubManager player)
    {
        player.Rb.isKinematic = true;
        player.transform.position = player._newGamePos;

    }

    public override void OnExitState(PlayerSubManager player)
    {
        player.Rb.isKinematic = false;

        player.Rb.DOMove(player._firstMapPos, player._startTween.Duration).SetEase(player._startTween.Ease).onComplete += () => { SaveManager.Instance.Save(); } ;
    }

    public override void OnFixedUpdateState(PlayerSubManager player)
    {
    }

    public override void OnUpdateState(PlayerSubManager player)
    {
        if (GameStartManager.Instance == null)
        {
            player.SwitchState(player.MovementState);
            return;
        }
        if (!GameStartManager.Instance.LockSubPosition && GameStartManager.Instance.IsGameStarted)
        {
            player.SwitchState(player.MovementState);
            return;
        }
    }
}
