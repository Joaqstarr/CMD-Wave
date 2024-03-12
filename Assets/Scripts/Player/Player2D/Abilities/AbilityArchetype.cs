using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void UseAbility(GameObject player, GameObject ability)
    {
        if (_data !=  null)
        {
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
}
