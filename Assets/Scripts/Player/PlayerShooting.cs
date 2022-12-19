using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerShooting : MonoBehaviour
{
    /*
        Handles the following:
        - Projectiles spinning around the wrist
        - Firing said projectiles
        - Reloading 
        - (Potentially) Loading in special rounds
    */
    [SerializeField] Animator anim;
    AudioSource sound;
    [SerializeField] AudioClip shootSFX;


    [SerializeField] List<GameObject> projectileIcicle;

    // Icicle display 
    [SerializeField] GameObject handReference;
    [SerializeField] List<GameObject> displayIcicle;
    List<GameObject> icicleList;
    [SerializeField] List<GameObject> beverages;

    // Ammo types
    public enum IcicleType
    {
        normal = 0,
        explosive = 1,
        piercing = 2
    }
    [SerializeField] IcicleType curIcicleType = IcicleType.normal;

    // Ammo count
    [SerializeField] int maxIcicles = 6;
    int icicleCount;

    // Fire rate
    [SerializeField] float fireDelay = 0.5f;
    float curDelay;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        curDelay = 0;
        icicleList = new List<GameObject>();
        Reload(curIcicleType);
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();
        if (Input.GetMouseButtonDown(0))
            Shoot(curIcicleType);
    }

    void HandleAnimations()
    {
        // Icicle spin
        float angleOffset = Mathf.PI * 2 / icicleList.Count;
        float angle = Time.realtimeSinceStartup % 2f * Mathf.PI * 2f;
        for (int i = 0; i < icicleList.Count; i++)
        {
            icicleList[i].transform.localPosition = new Vector3(Mathf.Sin(angle + angleOffset * i) * 0.12f, 0.35f, Mathf.Cos(angle + angleOffset * i) * 0.12f);
        }

        if (curDelay > 0)
        {
            curDelay -= Time.deltaTime;
        }
    }

    public void Reload(IcicleType type = 0)
    {
        for (int i = 0; i < icicleList.Count; i++)
        {
            Destroy(icicleList[i]);
        }
        icicleCount = maxIcicles;
        icicleList.Clear();
        float angleOffset = Mathf.PI * 2 / maxIcicles;
        for (int i = 0; i < maxIcicles; i++)
        {
            icicleList.Add(Instantiate(displayIcicle[(int)type], handReference.transform));
        }
    }

    // Attempts to shoot a projectile, if a cooldown is ready and ammo is available
    public void Shoot(IcicleType type = 0)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (curDelay > 0 || info.IsName("Reload"))
            return;


        if (!(info.IsName("AttackHold") || info.IsName("AttackStart")))
            anim.Play("AttackStart");

        anim.SetBool("Attack", true);
        if (icicleList.Count > 0)
        {
            sound.PlayOneShot(shootSFX);
            Instantiate(projectileIcicle[(int)type], icicleList[icicleCount - 1].transform.position, cameraController.self.camTrfm.rotation);
            Destroy(icicleList[icicleCount - 1]);
            icicleList.RemoveAt(icicleCount - 1);
            icicleCount--;
            curDelay = fireDelay;
        }
        else
        {
            anim.Play("Reload");
            Reload(curIcicleType);
        }
    }


    public void DrinkSetActive(bool active)
    {
        beverages[(int)curIcicleType].SetActive(active);
    }
}
