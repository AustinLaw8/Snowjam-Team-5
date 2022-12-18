using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MountableTower
{
    [SerializeField] Transform barrel;
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject bullet;

    float curDelay;
    [SerializeField] float chargeTime = 1f;
    float spinUpDuration;
    // Start is called before the first frame update
    void Start()
    {
        xaxis = barrel.localEulerAngles.y;
        yaxis = barrel.localEulerAngles.x;
        curDelay = 0;
        spinUpDuration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        yaxis = Mathf.Clamp(yaxis, -100f, -80f);
        xaxis = Mathf.Clamp(xaxis, -45f, 45f);

        barrel.localEulerAngles = new Vector3(yaxis, xaxis, 0);

        if (curDelay > 0)
            curDelay -= Time.deltaTime;
        if (spinUpDuration > 0)
        {
            spinUpDuration -= Time.deltaTime;
        }
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
            }
        }
    }
}
