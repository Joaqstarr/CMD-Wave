using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[CreateAssetMenu(menuName = "ScanDart/Scan Dart")]
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

    public override void UseAbility(GameObject player, GameObject ability)
    {
        GameObject dart = ability.transform.Find("ScanDartTransform").gameObject;
        ability.SetActive(true);
        dart.GetComponent<ScanDartTransform>().StartCoroutine(LaunchDart(player, ability, dart));
    }

    public override void OnActivationFailed()
    {
        RecallDarts(GameObject.Find("SubPlayer")); // just for testing - change later
    }
    public void RecallDarts(GameObject player)
    {
        foreach (GameObject dart in poolObjects)
        {
            dart.GetComponent<ScanDartParent>().RecallTransform();
            GameObject transform = dart.transform.Find("ScanDartTransform").gameObject;
            dart.transform.position = player.transform.position;
            transform.GetComponent<ScanDartTransform>()._dartVisuals.transform.position = dart.transform.position;
            transform.GetComponent<ScanDartTransform>().ResetDart();
            dart.SetActive(false);
        }
    }

    public IEnumerator LaunchDart(GameObject player, GameObject ability, GameObject dart)
    {
        yield return new WaitForFixedUpdate();
        dart.GetComponent<ScanDartTransform>().ResetDart();
        ability.transform.position = player.transform.position;

        ability.transform.Rotate(new Vector3(0, 0, SubViewCone.subAimAngle - (ability.transform.rotation.eulerAngles.z) - 90));
        dart.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * launchForce);
    }
}
