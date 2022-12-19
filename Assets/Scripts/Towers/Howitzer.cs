using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Howitzer : MountableTower
{
    [SerializeField] Transform barrel;
    [SerializeField] Transform support;
    [SerializeField] GameObject bullet;

    [SerializeField] float shootForce = 5f;
    float curDelay;
    Animator anim;
    AudioSource sound;

    [SerializeField] ParticleSystem[] ptclFX;
    
    void Start()
    {
        curDelay = 0;
        xaxis = support.localEulerAngles.y;
        yaxis = barrel.localEulerAngles.x;
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        yaxis = Mathf.Clamp(yaxis, 30f, 100f);

        barrel.localEulerAngles = new Vector3(yaxis, 0, 0);
        support.localEulerAngles = new Vector3(-90f, xaxis, 0);

        if (curDelay > 0)
            curDelay -= Time.deltaTime;
        
    }

    public override void Shoot()
    {
        if (curDelay > 0)
            return;
        GameObject shell = Instantiate(bullet, barrel.position + barrel.forward * 2f, barrel.rotation);
        shell.GetComponent<HowitzerShell>().ApplyForce(shootForce);
        anim.Play("Fire");
        curDelay = attackSpeed;
        sound.Play();

        for (int i = 0; i < 3; i++)
        {
            ptclFX[i].Play();
        }
        StartCoroutine(stopExhaustFX());
    }

    IEnumerator stopExhaustFX()
    {
        yield return new WaitForSeconds(8);
        ptclFX[2].Stop();
    }
}
