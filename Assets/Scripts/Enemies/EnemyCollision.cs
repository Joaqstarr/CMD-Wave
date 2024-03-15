using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public BaseEnemyManager Manager;

    PlayerSubHealth _playerSubHealth;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && !Manager._dead)
        {
            if (_playerSubHealth == null)
                _playerSubHealth = collision.gameObject.GetComponent<PlayerSubHealth>();

            _playerSubHealth.OnHit(Manager.BaseData.damage, Knockback(collision.transform.position), Manager.BaseData.stunDuration);
        }
    }

    private Vector3 Knockback(Vector3 playerPos)
    {
        return (playerPos - transform.position).normalized * Manager.BaseData.knockbackValue;
    }
}
