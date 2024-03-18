using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Probe")]
public class ProbeData : AbilityData
{
    [HideInInspector]
    public static bool ProbeDeployed;

    [Header("Probe")]
    public float launchForceValue;
    public int detonationDamage;
    public float detonationReturnTime;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velocityModifier;
    public float frictionModifier;
    //public float bounceModifier; unused currently - could be used to add bounce back from collisions

    [Header("Combat")]
    public int health;
    public float invulnTime;
    public int healthNoDrain = 10;
    public float healthDrainTickTime = 0.8f;

    [Header("View Cone")]
    public float fov;
    public int resolution;
    public float viewDistance;
    public int rayResolution;
    //public float blipGhostTime; // unnecessary? just use 1/sampleRate - more realistic/accurate
    public int sampleRate;
    public GameObject blipObject;
    public LayerMask collisionMask;
    public Material defaultColor;
    public Material enemyColor;
    public float cameraLookAhead = 5f;
    [Header("Sound")]
    [Range(0, 1)]
    public float minimumEngineVolume;

    public bool _isControlling;
    public bool _probeLoaded;

    public override void OnStart()
    {
        _probeLoaded = true;
        _isControlling = false;
        ProbeDeployed = false;
    }
    public override void UseAbility(GameObject player, GameObject ability)
    {
        if (_probeLoaded)
        {
            if (!_isControlling)
            {
                player.GetComponent<PlayerSubManager>().StartCoroutine(LaunchProbe(player, ability));
            }
            else
            {
                FirstPersonPlayerControls.Instance.UnPossess(ability.GetComponent<ProbeObject>()._probeControls);
                ability.GetComponent<ProbeObject>().Detonate();

            }
        }
        else
        {
            CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Probe Destroyed. Manual Reload Necessary."), false);
        }    
    }

    public void ReloadProbe()
    {
        _probeLoaded = true;
        CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Probe Loaded."), false);

    }

    private IEnumerator LaunchProbe(GameObject player, GameObject ability)
    {

        yield return new WaitForFixedUpdate();
        player.GetComponent<Collider>().enabled = false;
        ability.transform.parent = null;
        ProbeObject probe = ability.GetComponent<ProbeObject>();
        probe.gameObject.SetActive(true);
        ability.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * launchForceValue, ForceMode.Impulse);
        FirstPersonPlayerControls.Instance.Possess(ability.GetComponent<ProbeObject>()._probeControls, false);

        probe._probeVirtualCamera.Priority = 11;

        _isControlling = true;
        ProbeDeployed = true;

        yield return new WaitForSeconds(0.1f);
        player.GetComponent<Collider>().enabled = true;
    }
}
