using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private List<BaseEnemyHealth> _enemyHealths = new List<BaseEnemyHealth>();
    public static bool EnemyNearby = false;
    private void Start()
    {
        InvokeRepeating("CheckEnemyNearby", 1f, 0.8f);
    }
    private void CheckEnemyNearby()
    {
        int amt = 0;
        List<BaseEnemyHealth> healthsToRemove = new List<BaseEnemyHealth>();
        foreach(BaseEnemyHealth health in _enemyHealths)
        {
            if(!health.IsDead && !health.Invulnerable) amt++;
            else
            {
                healthsToRemove.Add(health);
            }
        }
        foreach(BaseEnemyHealth health in healthsToRemove)
        {
            _enemyHealths.Remove(health);
        }

        EnemyNearby = amt > 0;

    }
    private void OnTriggerEnter(Collider collision)
    {
        BaseEnemyHealth healthToAdd = collision.transform.GetComponentInParent<BaseEnemyHealth>();
        if(healthToAdd != null) 
        _enemyHealths.Add(collision.transform.GetComponentInParent<BaseEnemyHealth>());
    }
    private void OnTriggerExit(Collider collision)
    {
        _enemyHealths.Remove(collision.transform.GetComponentInParent<BaseEnemyHealth>());

    }
}
