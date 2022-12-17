using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    [SerializeField] private GameObject PROJECTILE_PREFAB;

    private Enemy target;

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
            projectile.transform.position = this.transform.position;
            projectile.transform.LookAt(target.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
}
