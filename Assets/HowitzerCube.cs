using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerCube : MonoBehaviour
{
    [SerializeField] GameObject iceExplosion;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform trfm, fxTrfm;
    [SerializeField] int spd;
    void Start()
    {
        rb.velocity = trfm.forward * spd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Instantiate()
        fxTrfm.parent = null;
        Destroy(fxTrfm.gameObject, 2);
    }
}
