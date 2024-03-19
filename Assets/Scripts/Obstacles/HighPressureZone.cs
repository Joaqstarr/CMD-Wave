using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HighPressureZone : MonoBehaviour, IDataPersistance
{
    public HighPressureZoneData _data;

    private float _timer;
    private bool _playerInZone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_playerInZone && _timer <= 0)
        {
            PlayerSubHealth.Instance.OnHit(_data.damage, Vector2.zero, 0, true);
            CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Danger: entering high pressure zone!"), false);
            _timer = _data.damageInterval;
        }

        if (_timer > 0)
            _timer -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSub"))
            if (!HighPressureRoom.HighPressureEquipped)
            {
                _playerInZone = true;
            }
            else
            {
                _playerInZone = false;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerSub"))
            if (!HighPressureRoom.HighPressureEquipped)
                _playerInZone = false;
    }

    public void SaveData(ref SaveData data)
    {
    }

    public void LoadData(SaveData data)
    {
        _timer = _data.damageInterval;

    }
}
