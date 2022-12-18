using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerShell : MonoBehaviour
{
    // public float speed = 2f;  // Must be set by spawner
    Rigidbody rb;
    int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyForce(float force)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7)
            Destroy(gameObject);
    }
}
