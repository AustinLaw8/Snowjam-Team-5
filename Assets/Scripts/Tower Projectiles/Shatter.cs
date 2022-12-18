using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    [SerializeField] float explosionForce = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dir;
        foreach (Transform child in transform)
        {
            dir.x = Random.Range(-2, 2);
            dir.y = Random.Range(-2, 2);
            dir.z = Random.Range(-2, 2);

            child.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, child.position + dir, 10f, Random.Range(-10, 10));
        }
        StartCoroutine(Cleanup());
    }

    IEnumerator Cleanup()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
