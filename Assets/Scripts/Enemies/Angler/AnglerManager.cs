using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerManager : BaseEnemyManager
{
    public BaseEnemyData Data;
    #region State References

    public BaseEnemyState AttackState = new AnglerSeekState();

    #endregion

    private float _deadDistance = 5f;
    public Rigidbody Rb;
    public Item ItemBait;
    private void Start()
    {
        IdleState = new AnglerIdleState();
        DeadState = new AnglerDeadState();
        
        base.Start();
        Rb = GetComponent<Rigidbody>();
        BaseData = Data;
        SwitchState(IdleState);

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Rb.isKinematic = true;
            Vector2 direction = (collision.contacts[0].point - transform.position).normalized;
            transform.DOMove((Vector2)transform.position + (direction * _deadDistance), 2f).SetEase(Ease.InCubic);
        }
    }


}
