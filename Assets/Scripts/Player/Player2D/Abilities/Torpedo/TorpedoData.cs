using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Torpedo")]
public class TorpedoData : AbilityData
{
    public float launchForce;
    public LayerMask collideWith;
    public int damage;

    private int reloadCounter;
    private bool canShoot = true;
    public override void OnStart()
    {
        reloadCounter = numToPool;
    }
    public override void UseAbility(GameObject player, GameObject ability)
    {
        if (canShoot)
        {
            ability.SetActive(true);
            ability.GetComponent<Torpedo>().StartCoroutine(LaunchTorpedo(player, ability));
        }

        reloadCounter--;
        if (reloadCounter <= 0)
        {
            canShoot = false;
        }
    }

    public override void OnActivationFailed()
    {
        Reload(GameObject.Find("SubPlayer")); // for testing - remove eventually
    }
    public override GameObject GetAbilityObject()
    {
        if (canShoot)
        {
            return poolObjects[numToPool - reloadCounter];
        }
        return null;
    }

    public void Reload(GameObject player)
    {
        foreach (GameObject torpedo in poolObjects)
        {
            torpedo.transform.position = player.transform.position;
            if (torpedo.activeInHierarchy)
                torpedo.SetActive(false);
        }

        canShoot = true;
        reloadCounter = numToPool;
    }

    public IEnumerator LaunchTorpedo(GameObject player, GameObject ability)
    {
        yield return new WaitForFixedUpdate();
        ability.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ability.transform.position = player.transform.position + SubViewCone.subAimVector;

        ability.transform.Rotate(new Vector3(0, 0, SubViewCone.subAimAngle - (ability.transform.rotation.eulerAngles.z) - 90));
        ability.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * launchForce);
    }
}
