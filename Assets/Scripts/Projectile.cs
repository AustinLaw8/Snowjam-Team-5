using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int dmg;
    [SerializeField] GameObject destroyFX;
    [SerializeField] protected Transform trfm;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] float maxDuration;
    // Start is called before the first frame update
    protected void Start()
    {
        if (!trfm) { trfm = transform; }
        Destroy(gameObject, maxDuration);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.GetComponent<HPEntity>().TakeDmg(dmg);
        }
        if (other.gameObject.layer != 7)
        {
            DestroySelf();
        }
    }

    protected void DestroySelf()
    {
        if (destroyFX) { Instantiate(destroyFX, trfm.position, trfm.rotation); }
        Destroy(gameObject);
    }

    
}
