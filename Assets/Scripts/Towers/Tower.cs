using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Target
{
    Closest, Farthest, First
}

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected float range;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected int cost;
    [SerializeField] protected GameManager gameManager;

    [SerializeField] protected Target targettingType;
    
    private float timer;

    void Start()
    {
        if (gameManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timer = attackSpeed;
        targettingType = Target.First;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    void Update()
    {
        if (gameManager.GetGameState() == GameState.Defending)
        {
            timer -= Time.deltaTime;
            if (timer < 0 && AttemptAttack())
            {
                timer = attackSpeed;
            }
        }
    }

    protected abstract bool AttemptAttack();

    protected Enemy GetClosestEnemy()
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

    protected Enemy GetFarthestEnemy()
    {
        float maxDist = float.NegativeInfinity;
        Enemy target = null;

        HashSet<Enemy> currentEnemies = gameManager.GetEnemies();
        if (currentEnemies == null) { return null; }

        foreach(Enemy enemy in currentEnemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (dist <= range && dist > maxDist)
            {
                maxDist = dist;
                target = enemy;
            }
        }
        return target;
    }

    protected List<Enemy> GetAllEnemiesInRange()
    {
        List<Enemy> targets = new List<Enemy>();
        HashSet<Enemy> currentEnemies = gameManager.GetEnemies();
        if (currentEnemies == null) { return null; }

        foreach(Enemy enemy in currentEnemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, this.transform.position);
            if (dist <= range)
            {
                targets.Add(enemy);
            }
        }
        return targets;
    }

    protected Enemy GetFirstEnemy()
    {
        float minRemainingDist = float.PositiveInfinity;
        Enemy target = null;

        HashSet<Enemy> currentEnemies = gameManager.GetEnemies();
        if (currentEnemies == null) { return null; }

        foreach(Enemy enemy in currentEnemies)
        {
            //float dist = enemy.GetPathDistance();
            float dist = 0; //IDK might not work
            if (dist <= range && dist < minRemainingDist)
            {
                minRemainingDist = dist;
                target = enemy;
            }
        }
        return target;
        
    }

    public int GetCost() { return cost; }
    public float GetRange() { return range; }
}
