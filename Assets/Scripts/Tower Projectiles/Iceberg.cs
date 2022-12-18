using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iceberg : MonoBehaviour
{
    [SerializeField] float duration = 6f;
    [SerializeField] GameObject shattered;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Exist());
    }

    IEnumerator Exist()
    {
        yield return new WaitForSeconds(duration);
        Instantiate(shattered, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
