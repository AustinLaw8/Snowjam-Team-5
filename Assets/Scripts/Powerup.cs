using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    PlayerShooting.IcicleType type;
    [SerializeField] GameObject explosive;
    [SerializeField] GameObject piercing;
    // Start is called before the first frame update
    void Start()
    {
        type = (PlayerShooting.IcicleType) Mathf.RoundToInt(Random.Range(1f, 2f));

        switch (type)
        {
            case PlayerShooting.IcicleType.explosive:
                explosive.SetActive(true);
                break;
            case PlayerShooting.IcicleType.piercing:
                piercing.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            other.gameObject.GetComponent<PlayerShooting>().Reload(type);
            Destroy(gameObject);
        } catch
        {

        }
    }
}
