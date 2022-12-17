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
            BasicTowerProjectile projectile = GameObject.Instantiate(PROJECTILE_PREFAB).GetComponent<BasicTowerProjectile>();
            projectile.Initialize(this.transform.position, target, damage);
            return true;
        }
        else
        {
            return false;
        }
    }
}
