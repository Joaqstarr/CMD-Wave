using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Scan Dart")]
public class ScanDartData : AbilityData
{
    public float launchForce;
    public LayerMask collideWith;

    [Header("Scan Area")]
    public int resolution;
    public float fov;
    public float viewDistance;
    public int rayResolution;
    public int sampleRate;
    public GameObject blipObject;
    public LayerMask collisionMask;
    public Material defaultColor;
    public Material enemyColor;

    private float _dartsReady;

    public override void OnStart()
    {
        _dartsReady = numToPool;
    }
    public override void UseAbility(GameObject player, GameObject ability)
    {
        _dartsReady--;
        CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Scan Dart Count: " + _dartsReady), false);
        GameObject dart = ability.transform.Find("ScanDartTransform").gameObject;
        ability.SetActive(true);
        dart.GetComponent<ScanDartTransform>().StartCoroutine(LaunchDart(player, ability, dart));
    }

    public override void OnActivationFailed()
    {
        CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Scan Darts Expended. Manual Reload Necessary."), false);
        //RecallDarts(GameObject.Find("SubPlayer")); // just for testing - change later
    }
    public override GameObject GetAbilityObject()
    {
        for (int i = 0; i < numToPool; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
            {
                return poolObjects[i];
            }
        }
        return null;
    }
    public void RecallDarts(GameObject player)
    {
        foreach (GameObject dart in poolObjects)
        {
            dart.GetComponent<ScanDartParent>().RecallTransform();
            GameObject transform = dart.transform.Find("ScanDartTransform").gameObject;
            Debug.Log("Starting recall");
            transform.GetComponent<ScanDartTransform>().ResetDart();
            dart.transform.position = player.transform.position;
            transform.GetComponent<ScanDartTransform>()._dartVisuals.transform.position = dart.transform.position;
            dart.SetActive(false);
        }
        _dartsReady = numToPool;
        CommandLineManager.Instance.OutputLine(CommandLineManager.StringToArray("Scan Darts at " + _dartsReady), false);

    }

    public IEnumerator LaunchDart(GameObject player, GameObject ability, GameObject dart)
    {
        yield return new WaitForFixedUpdate();
        dart.GetComponent<ScanDartTransform>().ResetDart();
        ability.transform.position = player.transform.position;

        ability.transform.Rotate(new Vector3(0, 0, SubViewCone.subAimAngle - (ability.transform.rotation.eulerAngles.z) - 90));
        dart.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * launchForce);
    }

    public bool IsLoaded
    {
        get { return _dartsReady == numToPool; }
    }
}
