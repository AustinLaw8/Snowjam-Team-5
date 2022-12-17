using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected int dmg;
    [SerializeField] GameObject destroyFX;
    [SerializeField] protected Transform trfm;
    [SerializeField] protected Rigidbody rb;
    // Start is called before the first frame update
    protected void Start()
    {
        if (!trfm) { trfm = transform; }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 6)
        {
            other.GetComponent<HPEntity>().TakeDmg(dmg);
        }
        DestroySelf();
    }

    protected void DestroySelf()
    {
        if (destroyFX) { Instantiate(destroyFX, trfm.position, trfm.rotation); }
        Destroy(gameObject);
    }
}
