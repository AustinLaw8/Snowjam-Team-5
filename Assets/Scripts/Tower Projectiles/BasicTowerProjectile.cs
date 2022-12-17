using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerProjectile : IcicleScript
{
    new protected void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.layer)
        {
            case 7:
                return;
            case 8:
                MobileEntity mobileEntity = other.GetComponent<MobileEntity>();
                mobileEntity.TakeDmg(dmg);
                mobileEntity.ApplySlow(.5f,100);
                mobileEntity.TakeKnockback(trfm.forward * 2);
                goto default;
            default:
                DestroySelf();
                break;

        }
    }
}
