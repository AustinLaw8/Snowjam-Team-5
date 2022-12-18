using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy_backup : MobileEntity
{
    private static float I_AM_HERE_THRESHHOLD = .3f;

    [SerializeField] protected int value;
    
    // GameManager will probably pass every enemy the nodes
    protected Queue<Vector3> movementNodes;
    protected GameManager gameManager;

    NavMeshAgent agent;
    int curGoal = 0;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = gameManager.goal[curGoal].position;
    }

    void FixedUpdate()
    {
        CheckGoal();
    }

    public float GetPathDistance()
    {
        return agent.remainingDistance;
    }

    void CheckGoal()
    {
        if (agent.remainingDistance < I_AM_HERE_THRESHHOLD)
        {
            curGoal++;
            if (curGoal >= gameManager.goal.Length)
            {

                // If popping pops the last node, it means we are at the end
                gameManager.DecreaseHealth(1);

                //gameManager.RemoveEnemy(this);
                Destroy(this.gameObject);
                return;
            } else
            {
                agent.destination = gameManager.goal[curGoal].position;
            }
        }
    } 

    protected override void Die()
    {
        gameManager.IncreaseCash(value);
        //gameManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }
}
