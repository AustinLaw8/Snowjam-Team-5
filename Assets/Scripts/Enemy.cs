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
        movementNodes = new Queue<Vector3>(gameManager.tempNodes);
        agent = GetComponent<NavMeshAgent>();
        agent.destination = gameManager.goal[curGoal].position;
    }

    void FixedUpdate()
    {
        CheckGoal();
        // Move();
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

    // TODO: Something about being frozen, or slowed, or something idk
    void Move()
    {
        Vector3 nextNode = Vector3.zero;
        while ( movementNodes.Count > 0 ) 
        {
            nextNode = movementNodes.Peek();

            // If we are at next node, pop it and try next
            if (Vector3.Distance(nextNode, this.transform.position) <= I_AM_HERE_THRESHHOLD)
            {
                Debug.Log("Node reached, popping");
                movementNodes.Dequeue();
                if (movementNodes.Count == 0)
                {
                    Debug.Log("End reached");

                    // If popping pops the last node, it means we are at the end
                    gameManager.DecreaseHealth(1);

                    gameManager.RemoveEnemy(this);
                    Destroy(this.gameObject);
                    return;
                }
            }
            else
            {
                break;
            }
        }
        
        // Turn towards next node if necessary
        this.transform.LookAt(nextNode);
        
        // This might not work properly, but walk towards next node
        addHorizontalVelocity(.2f,0,.7f,0);
        applyHorizontalFriction(friction);
    }
    
    protected override void Die()
    {
        gameManager.IncreaseCash(value);
        gameManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }

    public void SetNodes(Vector3[] nodes) { movementNodes = new Queue<Vector3>(nodes); }
}
