using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public BaseEnemyManager Manager;
    [SerializeField]
    private float _maxColisionTimer = 0;
    private float _timer = 0;
    private Collider _collider;
    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && !Manager._dead)
        {
            PlayerSubHealth.Instance.OnHit(Manager.BaseData.damage, Knockback(collision.transform.position), Manager.BaseData.stunDuration);
            _timer += Time.deltaTime;
            if(_maxColisionTimer > 0)
            if(_timer > _maxColisionTimer)
            {
                    StartCoroutine(IgnoreCollision(collision.collider));
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerSub") && !Manager._dead)
        {
            _timer = 0;
        }

    }
    private Vector3 Knockback(Vector3 playerPos)
    {
        return ((playerPos - transform.position) * 1000).normalized * Manager.BaseData.knockbackValue;
    }

    IEnumerator IgnoreCollision(Collider playerCol)
    {
        Physics.IgnoreCollision(playerCol, _collider);
        yield return new WaitForSeconds(1f);
        Physics.IgnoreCollision(playerCol, _collider, false);


    }
}
