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
    [SerializeField] PlayerShooting.IcicleType icicleType = 0;

    [Tooltip("For explosive icicle only. Defines explosion radius")]
    [SerializeField] float explosionRadius = 0.5f;

    // Start is called before the first frame update
    protected void Start()
    {
        if (!trfm) { trfm = transform; }
        Destroy(gameObject, maxDuration);
        rb.velocity = trfm.forward * spd;
    }

    protected void OnTriggerEnter(Collider other)
    {
        switch (icicleType)
        {
            case PlayerShooting.IcicleType.normal:
                if (other.gameObject.layer == 8)
                {
                    MobileEntity mobileEntity = other.GetComponent<MobileEntity>();
                    mobileEntity.TakeDmg(dmg);
                    mobileEntity.ApplySlow(.5f, 100);
                    mobileEntity.TakeKnockback(trfm.forward * 2);
                }
                DestroySelf();
                break;
            case PlayerShooting.IcicleType.explosive:
                foreach (Collider hit in Physics.OverlapSphere(gameObject.transform.position, explosionRadius, 1 << 8)) {
                    Debug.Log(hit);
                    MobileEntity mobileEntity = hit.gameObject.GetComponent<MobileEntity>();
                    mobileEntity.TakeDmg(dmg);
                    mobileEntity.ApplySlow(.5f, 100);
                    // mobileEntity.TakeKnockback(trfm.forward * 2);
                }
                DestroySelf();
                break;
            case PlayerShooting.IcicleType.piercing:
                if (other.gameObject.layer == 8)  // Enemy layer
                {
                    MobileEntity mobileEntity = other.GetComponent<MobileEntity>();
                    mobileEntity.TakeDmg(dmg);
                    mobileEntity.ApplySlow(.5f, 100);
                    mobileEntity.TakeKnockback(trfm.forward * 2);
                }
                if (other.gameObject.layer == 7 || other.gameObject.layer == 6)  // Terrain and Tower layers
                    DestroySelf();
                break;
        }

        
    }

    protected void DestroySelf()
    {
        if (destroyFX) { Instantiate(destroyFX, trfm.position, trfm.rotation); }
        Destroy(gameObject);
    }
}
