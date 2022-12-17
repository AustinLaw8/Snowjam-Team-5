using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleScript : MonoBehaviour
{
    [SerializeField] protected int dmg;
    [SerializeField] GameObject destroyFX;
    [SerializeField] protected Transform trfm;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] float spd, maxDuration;
    // Start is called before the first frame update
    protected void Start()
    {
        if (!trfm) { trfm = transform; }
        Destroy(gameObject, maxDuration);

        rb.velocity = trfm.forward * spd;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            MobileEntity mobileEntity = other.GetComponent<MobileEntity>();
            mobileEntity.TakeDmg(dmg);
            mobileEntity.ApplySlow(.5f,100);
            mobileEntity.TakeKnockback(trfm.forward * 4);
        }
        DestroySelf();
    }

    protected void DestroySelf()
    {
        if (destroyFX) { Instantiate(destroyFX, trfm.position, trfm.rotation); }
        Destroy(gameObject);
    }
}
