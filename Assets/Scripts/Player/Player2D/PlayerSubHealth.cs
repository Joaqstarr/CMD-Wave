using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubHealth : MonoBehaviour
{
    public PlayerSubData data;

    private PlayerSubManager _subManager;

    private float _health;
    private float _invulnTime;

    private bool _isInvuln;
    private float _invulnTimer;
    private bool _isStunned;


    void Start()
    {
        // component assignments
        _subManager = GetComponent<PlayerSubManager>();

        // variable assignments
        _health = data.health;
        _invulnTime = data.invulnTime;
    }

    // Update is called once per frame
    void Update()
    {
        IFrameCooldown();
    }

    public void OnHit(float damage, Vector2 knockback, float stunDuration)
    {
        if (!_isInvuln)
        {
            // damage, kb, invuln
            _health -= damage;
            Debug.Log("Current health: " + _health);
            _subManager.Rb.AddForce(knockback, ForceMode.Impulse);
            _isInvuln = true;
            _invulnTimer = _invulnTime;

            // stun
            if (stunDuration > 0)
            {
                _subManager._stunTimer = stunDuration;
            }
        }
    }

    public void IFrameCooldown()
    {
        if (_isInvuln)
        {
            _invulnTimer -= Time.deltaTime;

            if (_invulnTimer < 0)
            {
                Debug.Log("IFrames gone");
                _isInvuln = false;
            }
        }
    }
}
