using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AbilityArchetype : MonoBehaviour
{
    public AbilityData _data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if ( _data != null )
        {
            _data.OnStart();
        }
    }
    public void UseAbility(GameObject player, GameObject ability)
    {
        Debug.Log("Try fire ability");
        if (_data !=  null)
        {
            Debug.Log("Try fire ability now");

            _data.UseAbility(player, ability);
        }
    }

    public void UseAbility(GameObject player)
    {
        if (_data != null)
        {
            _data.UseAbility(player);
        }
    }

    public void OnActivationFailed()
    {
        if (_data != null )
        {
            _data.OnActivationFailed();
        }
    }

    public void OnActivationFailed(GameObject player)
    { 
        if (_data != null)
        {
            _data.OnActivationFailed(player);
        } 
    }

    public GameObject GetAbilityObject()
    {
        if ( _data != null)
        {
            return _data.GetAbilityObject();
        }
        return null;
    }
}
