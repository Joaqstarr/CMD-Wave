using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager Main;

    public AbilityArchetype[] _allAbilities;
    public AbilityArchetype _activeAbility;
    private GameObject _probe;
    private GameObject _player;
    private enum AbilityState {
        ready,
        cooldown
    }

    private AbilityState _state = AbilityState.ready;

    private bool _inputHeld = false;
    private bool _inputHeldLastFrame = false;
    private float _cooldownTimer;

    private void Awake()
    {
        Main = this;
    }
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
                holder.transform.position = this.transform.position;
                GameObject tempObject;
                for (int i = 0;  i < ability._data.numToPool; i++)
                {
                    tempObject = Instantiate(ability._data.abilityPrefab, holder.transform);
                    tempObject.SetActive(false);
                    ability._data.poolObjects[i] = tempObject;
                }
            }
        }

        if (_allAbilities[0] != null)
        {
            _activeAbility = _allAbilities[0];
        }
        else
        {
            _activeAbility = null;
        }


        _player = transform.parent.gameObject;
        _probe = transform.Find("ProbeObject").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (_state)
        {
            case AbilityState.ready:
                if (_activeAbility != null)
                {
                    if ((PlayerSubControls.Instance.PowerPressed || (_activeAbility._data.commandShortcut == "pb" && ProbeControls.Instance.PowerPressed) && !_inputHeld || (_activeAbility._data.commandShortcut == "da" && (_inputHeld || _inputHeldLastFrame))))
                    {
                        if (_activeAbility._data.numToPool > 0)
                        {
                            GameObject ability = GetAbilityObject(_activeAbility);
                            if (ability != null)
                            {
                                _activeAbility.UseAbility(_player, ability);
                            }
                            else
                            {
                                _activeAbility.OnActivationFailed();
                            }
                        }
                        else
                        {
                            if (_activeAbility._data.commandShortcut == "pb")
                            {
                                _activeAbility.UseAbility(_player, _probe);
                            }
                            else
                            {
                                _activeAbility.UseAbility(_player);
                            }
                        }
                        _inputHeldLastFrame = _inputHeld;
                        _inputHeld = PlayerSubControls.Instance.PowerPressed;
                        if (!(_activeAbility._data.commandShortcut == "da") || (_inputHeldLastFrame && !_inputHeld))
                        {
                            _cooldownTimer = _activeAbility._data.cooldown;

                            if (_activeAbility._data.commandShortcut == "da")
                                _activeAbility._data.UseOnRelease(_player);
                        }
                    }
                    else if (!PlayerSubControls.Instance.PowerPressed)
                    {
                        _inputHeld = false;
                    }

                    if (_cooldownTimer > 0)
                        _state = AbilityState.cooldown;
                    break;
                }
                else
                    break; // command terminal error message?

            case AbilityState.cooldown:
                if (_cooldownTimer > 0)
                {
                    _cooldownTimer -= Time.deltaTime;
                    _inputHeldLastFrame = false;
                }
                else
                {
                    _state = AbilityState.ready;
                }
                break;
        }
    }

    public void Equip(AbilityArchetype ability)
    {
        _activeAbility = ability;
    }

    public bool Equip(string ab)
    {
        foreach (AbilityArchetype ability in _allAbilities)
        {
            if (ability._data.commandShortcut.ToLower().Equals(ab.ToLower()))
            {
                _activeAbility = ability;
                return true;
            }
        }
        return false;
    }

    public void UpdateAbilities()
    {
        _allAbilities = GetComponentsInChildren<AbilityArchetype>();
    }

    private GameObject GetAbilityObject(AbilityArchetype ability)
    {
        return ability.GetAbilityObject();
    }
}
