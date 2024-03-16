using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemyManager : BaseEnemyManager, IKnockbackable
{
    [SerializeField]
    public ChargeEnemyData _data;

    #region State References

    public BaseEnemyState SeekState = new ChargeEnemySeekState();
    public BaseEnemyState AttackState = new ChargeEnemyAttackState();

    #endregion

    #region ComponentReferences
    public Rigidbody Rb { get; private set; }

    #endregion

    #region Variables
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public float chargeCooldownTimer = 0;
    [HideInInspector]
    public Vector2 _startPosition;

    private float _deadDistance = 3;

    #endregion
    void Start()
    {
        IdleState = new ChargeEnemyIdleState();
        //DeadState = new ChargeEnemyDeadState();
        // set components
        Rb = GetComponent<Rigidbody>();

        // find player
        Player = GameObject.Find("SubPlayer");

        BaseData = Data;
        _startPosition = transform.position;

        base.Start();

        // movement state
        SwitchState(IdleState);

        // enter state
        CurrentState.OnEnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdateState(this);
    }

    public void Knockback(float force, float stunDuration, Vector3 origin)
    {
        Rb.velocity = Vector3.zero;
        Debug.Log(transform.position - origin);
        float tempAngle = Mathf.Atan2(transform.position.y - origin.y, transform.position.y - origin.x) * Mathf.Rad2Deg;
        Vector3 collisionDir = new Vector3(Mathf.Cos(tempAngle * (Mathf.PI / 180f)), Mathf.Sin(tempAngle * (Mathf.PI / 180f)));
        Rb.AddForce((collisionDir) * force, ForceMode.Impulse);
        Debug.Log("Force: " + (transform.position - origin) * force);
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

    public ChargeEnemyData Data
    {
        get { return _data; }
    }
}
