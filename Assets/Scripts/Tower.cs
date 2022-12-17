using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float cost;

    private float timer;

    void Start()
    {
        timer = attackSpeed;
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer < 0)
        {
            // Enemy enemy = GetClosestEnemy();
            // if (enemy) {
                // Attack(enemy);
                // timer = attackSpeed;
            // }
        }
    }

    // void Attack(Enemy enemy)
    // {
            // do something interesting, probably
    // }

    // TODO: look into different targeting styles
    // Enemy GetClosestEnemy()
    // {
            // TODO: I'm gonna assume theres a list of enemies somewhere so we can do targeting.
            // do something useful, probably
    // }

    float GetCost() { return cost; }
    float GetRange() { return range; }
}
