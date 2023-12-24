using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MountableTower
{
    [SerializeField] Transform barrel;
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject bullet;
    [SerializeField] [Tooltip("for animation")] GameObject spinBarrel;
    [SerializeField] [Range(0, 10f)] float maxRevRate = 2.5f;

    float curDelay;
    [SerializeField] float chargeTime = 1f;
    float spinUpDuration;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        xaxis = barrel.localEulerAngles.y;
        yaxis = barrel.localEulerAngles.x;
        curDelay = 0;
        spinUpDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        yaxis = Mathf.Clamp(yaxis, -30f, 0f);
        // xaxis = Mathf.Clamp(xaxis, -45f, 45f);

        barrel.localEulerAngles = new Vector3(yaxis, xaxis, 0);

        if (curDelay > 0)
            curDelay -= Time.deltaTime;
        else
            curDelay = 0;
        if (spinUpDuration > 0)
        {
            spinUpDuration -= Time.deltaTime;
        }

        spinBarrel.transform.Rotate(Vector3.up, Mathf.Pow(spinUpDuration / chargeTime, 2) * Mathf.PI * 2 * maxRevRate);
    }

    public override void Shoot()
    {
        if (spinUpDuration < chargeTime)
        {
            spinUpDuration += Time.deltaTime * 2;
        } else
        {
            spinUpDuration = chargeTime + Time.deltaTime * 2;
            // Fire
            if (curDelay <= 0)
            {
                Instantiate(bullet, spawnpoint.position, spawnpoint.rotation);
                curDelay = attackSpeed;
                sound.Play();
            }
        }
    }
}
