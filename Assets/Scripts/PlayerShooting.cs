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


    [SerializeField] GameObject projectileIcicle;

    // Icicle display 
    [SerializeField] GameObject handReference;
    [SerializeField] GameObject displayIcicle;
    List<GameObject> icicleList;
    [SerializeField] List<GameObject> beverages;

    // Ammo count
    [SerializeField] int maxIcicles = 6;
    int icicleCount;

    // Fire rate
    [SerializeField] float fireDelay = 0.5f;
    float curDelay;

    void Start()
    {
        curDelay = 0;
        icicleList = new List<GameObject>();
        Reload();
    }

    // Update is called once per frame
    void Update()
    {
        HandleAnimations();
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void HandleAnimations()
    {
        // Icicle spin
        float angleOffset = Mathf.PI * 2 / icicleList.Count;
        float angle = Time.realtimeSinceStartup % 2f * Mathf.PI * 2f; 
        for (int i = 0; i < icicleList.Count; i++)
        {
            icicleList[i].transform.localPosition = new Vector3(Mathf.Sin(angle + angleOffset * i) * 0.1f, 0.35f, Mathf.Cos(angle + angleOffset * i) * 0.1f);
        }

        if (curDelay > 0)
        {
            curDelay -= Time.deltaTime;
        }
    }

    public void Reload()
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
            icicleList.Add(Instantiate(displayIcicle, handReference.transform));
            // icicleList[i].transform.localPosition = new Vector3(Mathf.Sin(angleOffset * i), 0, Mathf.Cos(angleOffset * i)) * 0.1f;
        }
    }

    // Attempts to shoot a projectile, if a cooldown is ready and ammo is available
    public void Shoot(int type = 0)
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        if (curDelay > 0 || info.IsName("Reload"))
            return;

        
        if (!(info.IsName("AttackHold") || info.IsName("AttackStart")))
            anim.Play("AttackStart");

        anim.SetBool("Attack", true);
        if (icicleList.Count > 0)
        {
            Instantiate(projectileIcicle, icicleList[icicleCount - 1].transform.position, cameraController.self.camTrfm.rotation);
            Destroy(icicleList[icicleCount - 1]);
            icicleList.RemoveAt(icicleCount - 1);
            icicleCount--;
        } else
        {
            anim.Play("Reload");
            Reload();
        }
    }

    public void DrinkSetActive(bool active)
    {
        beverages[0].SetActive(active);
    }
}
