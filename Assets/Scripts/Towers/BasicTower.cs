using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [SerializeField] private GameObject PROJECTILE_PREFAB;
    [SerializeField] Transform gun;
    [SerializeField] Transform spawnpoint;

    private Enemy target;

    private void OnDestroy()
    {
        Debug.Log("ahhhh im dead");
    }
    protected override bool AttemptAttack()
    {
        switch (targettingType)
        {
            case Target.Closest:
                target = GetClosestEnemy();
                break;
            case Target.Farthest:
                target = GetFarthestEnemy();
                break;
            case Target.First:
            default:
                target = GetFirstEnemy();
                break;
        }

        if (target)
        {
            GameObject projectile = GameObject.Instantiate(PROJECTILE_PREFAB);
            projectile.transform.position = spawnpoint.position;
            projectile.transform.LookAt(target.transform.position);
            gun.transform.LookAt(target.transform.position);
            GetComponent<AudioSource>().Play();
            return true;
        }
        else
        {
            return false;
        }
    }
}
