using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubHealth : MonoBehaviour, IDataPersistance
{
    public PlayerSubData data;

    private PlayerSubManager _subManager;

    private int _health;
    private float _invulnTime;

    private bool _isInvuln;
    private float _invulnTimer;
    private bool _isStunned;

    private CinemachineImpulseSource _shakeSource;

    public delegate void PlayerHitDel(float strength);
    public static PlayerHitDel OnHitDel;
    public static PlayerHitDel OnDeathDel;
    public static PlayerSubHealth Instance;

    [SerializeField]
    private AudioClip[] _hitClips;
    private AudioSource _audioSource;
    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        // component assignments
        _subManager = GetComponent<PlayerSubManager>();

        // variable assignments
        _health = data.health;
        _invulnTime = data.invulnTime;
        _shakeSource = GetComponent<CinemachineImpulseSource>();
        InvokeRepeating("TickDamage", data.healthDrainTickTime, data.healthDrainTickTime);

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IFrameCooldown();
    }

    public void OnHit(int damage, Vector2 knockback, float stunDuration, bool damageRoom = true)
    {
        bool hittingWall = damage == 0 && damageRoom == false;

        if (!_isInvuln || hittingWall)
        {
            float strength = 1;
            // damage, kb, invuln
            _health -= damage;
            Debug.Log("Current health: " + _health);
            _subManager.Rb.AddForce(knockback, ForceMode.Impulse);
            if (!hittingWall)
            {
                _isInvuln = true;
                _invulnTimer = _invulnTime;
            }
            else
            {
                strength = 0.2f;
            }


            if (_shakeSource != null)
                _shakeSource.GenerateImpulseWithForce(strength);

            if (VesselRoomHandler.Instance != null && damageRoom)
                VesselRoomHandler.Instance.DamageRoom();


            PlayHitSound(strength);


            if (OnHitDel != null)
            {
                OnHitDel(strength);
            }
            // stun
            if (stunDuration > 0)
            {
                _subManager._stunTimer = stunDuration;
            }

            if(_health <= 0)
            {
                Debug.Log("Dead");
                _health = 0;
                KillSequence();
            }
        }
    }

    public void TickDamage()
    {

        if (VesselRoomHandler.Instance != null)
        {
            int dmg = VesselRoomHandler.Instance.DamageAmount;
            if (dmg == 0)
            {
                _health = Mathf.Min(data.health, _health + VesselRoomHandler.Instance.RoomCount);
            }

            float strength = 0.01f * dmg;

            if (_shakeSource != null)
                _shakeSource.GenerateImpulseWithForce(strength);

            if(_health > 10)
            {
                int newHealth = Mathf.Max(_health - dmg, data.healthNoDrain);
                _health = newHealth;

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

    private void PlayHitSound(float vol = 1)
    {
        if(_audioSource != null)
        {
            int ran = Random.Range(0, _hitClips.Length);
            AudioClip audioClip = _hitClips[ran];

            _audioSource.PlayOneShot(audioClip, vol);
        }
    }

    public void SaveData(ref SaveData data)
    {
        data._subHealth = _health;
    }

    public void LoadData(SaveData data)
    {
        _health = data._subHealth;
    }

    public int Health
    {
        get { return _health; }
    }

    public void KillSequence()
    {
        if (DeathScreenManager.Instance != null)
            if (DeathScreenManager.Instance.IsDead || DeathScreenManager.Instance.IsDying) return;

        if (DeathScreenManager.Instance != null)
            DeathScreenManager.Instance.SetDead();

            FirstPersonPlayerControls.Instance.UnPossess(); 
        if (OnDeathDel != null)
            OnDeathDel(4);
        StartCoroutine(WaitForDeathTime());
        
    }

    IEnumerator WaitForDeathTime()
    {
 
        yield return new WaitForSeconds(4f);
        if (_health <= 0)
        if (DeathScreenManager.Instance != null)
            DeathScreenManager.Instance.KillScreen();

    }
}
