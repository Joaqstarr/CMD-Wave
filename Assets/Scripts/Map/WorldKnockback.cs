using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldKnockback : MonoBehaviour
{
    [SerializeField] private float _knockback = 10;
    private PlayerSubHealth _playerHealth;
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub"))
        {
            if (_playerHealth == null)
                _playerHealth = collision.gameObject.GetComponent<PlayerSubHealth>();


            Vector3 knockback = (collision.transform.position - collision.contacts[0].point).normalized * _knockback;

            _playerHealth.OnHit(0, knockback, 0, false);
        }
    }
}
