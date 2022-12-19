using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airdrop : MonoBehaviour
{
    [SerializeField] GameObject powerup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Instantiate(powerup, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
