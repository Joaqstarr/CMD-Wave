using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseEnemyHealth : MonoBehaviour, IHittable
{
    private int _health;
    [SerializeField]
    private BaseEnemyData _enemyData;

    public UnityEvent OnHit;
    private bool _dead = false;
    // Start is called before the first frame update
    void Start()
    {
        _health = _enemyData.health;
    }

    public void Hit(int damage, GameObject from)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            Kill();
        }
        else
        {
            if(OnHit != null)
            {
                OnHit.Invoke();
            }
        }
    }

    public void Kill()
    {
        _dead = true;
    }



    public bool IsDead
    {
        get{ return _dead; }
    }

    public void Revive()
    {
        _health = _enemyData.health;

        _dead = false;
    }


}
