using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityManager : MonoBehaviour
{
    public AbilityArchetype[] _allAbilities;
    public AbilityArchetype _activeAbility;
    public KeyCode _key; //for testing
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
                ability._data.poolObjects = new GameObject[ability._data.numToPool];
                GameObject holder = new GameObject(ability.name + "Holder");
                holder.transform.parent = this.transform;
                GameObject tempObject;
                for (int i = 0;  i < ability._data.numToPool; i++)
                {
                    tempObject = Instantiate(ability._data.abilityPrefab, holder.transform);
                    tempObject.SetActive(false);
                    ability._data.poolObjects[i] = tempObject;
                }
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
                {
                    if (_activeAbility._data.numToPool > 0)
                    {
                        GameObject ability = GetAbilityObject(_activeAbility);
                        if (ability != null)
                        {
                            _activeAbility.UseAbility(_player.gameObject, ability);
                        }
                        else
                        {
                            _activeAbility.OnActivationFailed();
                        }
                    }
                    else
                    {
                        _activeAbility.UseAbility(_player.gameObject);
                    }
                }
                    break;

            case AbilityState.cooldown:
                break;
        }
    }

    public void Equip(AbilityArchetype ability)
    {
        _activeAbility = ability;
    }

    private GameObject GetAbilityObject(AbilityArchetype ability)
    {
        for (int i = 0; i < ability._data.numToPool; i++)
        {
            if (!ability._data.poolObjects[i].activeInHierarchy)
            {
                return ability._data.poolObjects[i];
            }
        }
        return null;
    }
}
