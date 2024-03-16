using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShyEnemySeekState : ShyEnemyBaseState
{
    public override void OnEnterState(ShyEnemyManager enemy)
    {
        enemy.Pathfinder.canMove = true;
        Debug.Log("Seeking");
        enemy.DestinationSetter.target = enemy._player.transform;
    }

    public override void OnExitState(ShyEnemyManager enemy)
    {

    }

    public override void OnUpdateState(ShyEnemyManager enemy)
    {
        // stop moving if in player view cone
        Vector3 playerDistance = enemy.transform.position - enemy._player.transform.localPosition;
        float angleToPlayer = Mathf.Atan2(playerDistance.y, playerDistance.x) * Mathf.Rad2Deg;
        if (angleToPlayer < 0) angleToPlayer += 360;
        float adjustedSubAim = SubViewCone.subAimAngle;
        if ((angleToPlayer <= enemy._playerData.fov/2) && (adjustedSubAim >= (360 - enemy._playerData.fov / 2)))
            adjustedSubAim -= 360;
        if (angleToPlayer > adjustedSubAim - (enemy._playerData.fov / 2) && angleToPlayer < adjustedSubAim + (enemy._playerData.fov / 2))
        {
            if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) <= enemy._playerData.viewDistance)
                enemy.StartCoroutine(StunInView(enemy));
            else
                enemy.Pathfinder.canMove = true;
        }
        else
            enemy.Pathfinder.canMove = true;

        // switch state conditionals

        // idle
        if (Vector3.Distance(enemy.transform.position, enemy._player.transform.position) > enemy.EnemyData.detectionRadius)
            enemy.SwitchState(enemy.IdleState);
        else if (Physics.Linecast(enemy.transform.position, enemy._player.transform.position, 9))
            enemy.SwitchState(enemy.IdleState);
    }

    public override void OnFixedUpdateState(ShyEnemyManager enemy)
    {

    }

    private IEnumerator StunInView(ShyEnemyManager enemy)
    {
        yield return new WaitForSeconds(enemy.EnemyData.stunReactionTime);
        enemy.Pathfinder.canMove = false;
    }
}
