using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockbackable
{
    public void Knockback(float force, float stunTime, Vector3 origin);
}
