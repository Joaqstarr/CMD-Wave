using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentPath : MonoBehaviour
{

    public Collider Currentcollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerSub")
        {
            Physics.IgnoreCollision(Currentcollider, other, true);

        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerSub")
        {
            Physics.IgnoreCollision(Currentcollider, other, true);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerSub")
        {
            Physics.IgnoreCollision(Currentcollider, other, false);
        }
    }
}
