using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public BaseEnemyManager Manager;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && !Manager._dead)
        {

            PlayerSubHealth.Instance.OnHit(Manager.BaseData.damage, Knockback(collision.transform.position), Manager.BaseData.stunDuration);

        }
    }

    private Vector3 Knockback(Vector3 playerPos)
    {
        return ((playerPos - transform.position) * 1000).normalized * Manager.BaseData.knockbackValue;
    }
}
