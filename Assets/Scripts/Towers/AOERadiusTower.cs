using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOERadiusTower : Tower
{
    protected override bool AttemptAttack()
    {
        List<Enemy> targets = GetAllEnemiesInRange();
        if (targets.Count > 0)
        {
            Debug.Log("Attracking");

            foreach (Enemy enemy in targets)
            {
                enemy.TakeDmg(damage);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
