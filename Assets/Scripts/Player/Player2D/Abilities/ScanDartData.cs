using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScanDart/Scan Dart")]
public class ScanDartData : AbilityData
{
    public float launchForce;
    public LayerMask collideWith;
    private GameObject[] transforms;

    public override void UseAbility(GameObject player, GameObject ability)
    {
        if (transforms[0] == null)
        {
            Debug.Log("test");
            transforms = new GameObject[numToPool];

            for (int i = 0; i < poolObjects.Length; i++) 
            {
                transforms[i] = poolObjects[i].transform.Find("ScanDartTransform").gameObject;
            }
        }

        GameObject dart = ability.transform.Find("ScanDartTransform").gameObject;
        dart.GetComponent<ScanDartTransform>().ResetDart();
        ability.SetActive(true);
        Debug.Log("fire");
        ability.transform.position = player.transform.position;

        ability.transform.Rotate(new Vector3(0, 0, SubViewCone.subAimAngle - (ability.transform.rotation.eulerAngles.z) - 90));
        dart.GetComponent<ScanDartTransform>().StartCoroutine(ApplyForce(dart));
    }

    public override void OnActivationFailed()
    {
        RecallDarts(GameObject.Find("SubPlayer")); // just for testing - change later
    }
    public void RecallDarts(GameObject player)
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            transforms[i].transform.parent = poolObjects[i].transform;
        }

        foreach (GameObject dart in poolObjects)
        {
            GameObject transform = dart.transform.Find("ScanDartTransform").gameObject;
            dart.transform.position = player.transform.position;
            transform.GetComponent<ScanDartTransform>()._dartUI.transform.position = dart.transform.position;
            transform.GetComponent<ScanDartTransform>().ResetDart();
            dart.SetActive(false);
        }
    }

    public IEnumerator ApplyForce(GameObject dart)
    {
        yield return new WaitForFixedUpdate();
        dart.GetComponent<Rigidbody>().AddForce(SubViewCone.subAimVector * launchForce);
        //dart.transform.position += SubViewCone.subAimVector * 10;
    }
}
