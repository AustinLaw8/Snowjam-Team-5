using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MobileEntity
{
    private static float I_AM_HERE_THRESHHOLD = .3f;

    [SerializeField] protected int hp;
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
        // Move();
        string outstr = "";
        foreach(Vector3 coords in agent.path.corners)
        {
            outstr += coords.ToString();
        }
        Debug.Log(outstr);
    }

    public float GetPathDistance()
    {
        return agent.remainingDistance;
    }

    public void TakeDmg(int dmg)
    {

    }

    void CheckGoal()
    {
        if (agent.remainingDistance < I_AM_HERE_THRESHHOLD)
        {
            curGoal++;
            if (curGoal >= gameManager.goal.Length)
            {
                Debug.Log("End reached");

                // If popping pops the last node, it means we are at the end
                gameManager.DecreaseHealth(1);

                gameManager.RemoveEnemy(this);
                Destroy(this.gameObject);
                return;
            } else
            {
                agent.destination = gameManager.goal[curGoal].position;
            }
        }
    } 
    
    protected void Die()
    {
        gameManager.IncreaseCash(value);
        gameManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }
}
