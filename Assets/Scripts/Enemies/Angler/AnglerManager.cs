using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerManager : BaseEnemyManager, IKnockbackable
{
    public BaseEnemyData Data;
    #region State References

    public BaseEnemyState AttackState = new AnglerSeekState();

    #endregion

    private float _deadDistance = 5f;
    public Rigidbody Rb;
    public Item ItemBait;
    public Vector2 StartLocation {  get; private set; }
    public Vector2 StartItemLocation { get; private set; }

    private void Start()
    {
        IdleState = new AnglerIdleState();
        DeadState = new AnglerDeadState();
        
        base.Start();
        Rb = GetComponent<Rigidbody>();
        BaseData = Data;
        SwitchState(IdleState);
        StartLocation = transform.position;
        StartItemLocation = ItemBait.transform.position;

        
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
    public void Hit()
    {
        if(CurrentState == IdleState && !_dead)
        {
            SwitchState(AttackState);
        }
    }

    public void Knockback(float force, float stunDuration, Vector3 origin)
    {
        Rb.velocity = Vector3.zero;
        Vector3 distanceVector = (transform.position - origin) * 100;
        Debug.Log(distanceVector.normalized);
        Rb.AddForce(distanceVector.normalized * (force * 0.7f), ForceMode.Impulse);
        Debug.Log("Force: " + ((distanceVector.normalized) * force));
    }

}
