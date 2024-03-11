using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public AbilityArchetype[] _allAbilities;
    public AbilityArchetype _activeAbility;
    public KeyCode _key; //for testing
    public GameObject _dart; // for testing
    private GameObject _player;
    private enum AbilityState {
        ready,
        cooldown
    }

    private AbilityState _state = AbilityState.ready;

    private void Start()
    {
        _allAbilities = GetComponentsInChildren<AbilityArchetype>();
        foreach (AbilityArchetype ability in _allAbilities)
        {
            if (ability._data.numToPool > 0)
            {
                GameObject holder = new GameObject(ability.name + "Holder");
                holder.transform.parent = this.transform;
                for (int i = 0;  i < ability._data.numToPool; i++)
                {
                    Instantiate(ability._data.abilityPrefab, holder.transform);
                }
                holder.SetActive(false);
            }
        }
        _player = transform.parent.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(_key))
                    _activeAbility.UseAbility(_player.gameObject, _dart);
                    break;

            case AbilityState.cooldown:
                break;
        }
    }

    public void Equip(AbilityArchetype ability)
    {
        _activeAbility = ability;
    }
}
