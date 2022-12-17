using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float cost;
    [SerializeField] protected int damage;
    [SerializeField] protected GameManager gameManager;

    private float timer;

    void Start()
    {
        if (gameManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = attackSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    void FixedUpdate()
    {
        if (gameManager.waveInProgress && gameManager.controllable)
        {
            timer -= Time.fixedDeltaTime;
            if (timer < 0)
            {
                Enemy target = GetClosestEnemy();
                if (target) {
                    Attack(target);
                    timer = attackSpeed;
                }
            }
        }
    }

    void Attack(Enemy target)
    {
        Debug.Log($"Attacking enemy for {damage} damage" );
        target.damage(damage);
    }

    // TODO: look into different targeting styles

    /*
    Enemy GetClosestEnemy()
    {
        float minDist = float.PositiveInfinity;
        Enemy target = null;

        HashSet<Enemy> currentEnemies = gameManager.GetEnemies();
        if (currentEnemies == null) { return null; }

        foreach(Enemy enemy in currentEnemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (dist <= range && dist < minDist)
            {
                minDist = dist;
                target = enemy;
            }
        }
        return target;
    }
    */

    float GetCost() { return cost; }
    float GetRange() { return range; }
}
