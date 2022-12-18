using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowitzerShell : MonoBehaviour
{
    // public float speed = 2f;  // Must be set by spawner
    Rigidbody rb;
    int damage;
    [SerializeField] GameObject iceberg;
    [SerializeField] int freezeduration = 6;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyForce(float force, int dmg = 10)
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
        damage = dmg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7)
        {
            // Explode and freeze enemies 
            foreach (Collider hit in Physics.OverlapSphere(gameObject.transform.position, 1.2f, 1 << 8))
            {
                Debug.Log(hit);
                MobileEntity mobileEntity = hit.gameObject.GetComponent<MobileEntity>();
                mobileEntity.TakeDmg(damage);
                mobileEntity.ApplySlow(1f, 50 * freezeduration);
            }

            if (other.gameObject.tag == "Navmesh")  // Hits ground 
            {
                // Spawn iceberg at point of collision
                Instantiate(iceberg, transform.position, Quaternion.identity);
            } 
            else if (other.gameObject.layer == 8)  // Hits enemy
            {
                // Raycast directly down for the terrain, then spawn Iceberg
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit);
                Instantiate(iceberg, hit.point, Quaternion.identity);
            }

            Destroy(gameObject);
        }
            
    }
}
