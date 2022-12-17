using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleScript : Projectile
{
    [SerializeField] float spd;

    new void Start()
    {
        base.Start();
        rb.velocity = trfm.forward * spd;
    }
}
