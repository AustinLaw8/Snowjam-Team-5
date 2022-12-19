using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MobileEntity
{
    private static float I_AM_HERE_THRESHHOLD = 4f;

    [SerializeField] protected int value;

    // GameManager will probably pass every enemy the nodes
    protected Queue<Vector3> movementNodes;
    protected GameManager gameManager;

    int currentTargetNode = 0;
    int curGoal = 0;

    int nodeCheckDelay;

    [SerializeField] float speed, acceleration;

    void Start()
    {
        HP = maxHP;
        gameManager = GameManager.self;
    }

    void FixedUpdate()
    {
        if (nodeCheckDelay < 1)
        {
            nodeCheckDelay = 25;
            UpdateNode();
        }
        else
        {
            nodeCheckDelay--;
        }

        addHorizontalVelocity(acceleration, 0, speed, 0);
        applyHorizontalFriction(friction);
    }

    void UpdateNode()
    {
        trfm.LookAt(gameManager.nodes[currentTargetNode].position);

        if (Vector3.Distance(trfm.position, gameManager.nodes[currentTargetNode].position) < I_AM_HERE_THRESHHOLD)
        {
            currentTargetNode++;
            if (currentTargetNode == gameManager.nodes.Length)
            {
                gameManager.DecreaseHealth(1);

                Debug.Log("MADE IT BRO");

                Destroy(this.gameObject);
                return;
            }
        }
    }

    void CheckGoal()
    {
        //if (agent.remainingDistance < I_AM_HERE_THRESHHOLD)
        {
            curGoal++;
            if (curGoal >= gameManager.goal.Length)
            {

                // If popping pops the last node, it means we are at the end
                gameManager.DecreaseHealth(1);

                //gameManager.RemoveEnemy(this);
                Destroy(this.gameObject);
                return;
            }
            else
            {
                //agent.destination = gameManager.goal[curGoal].position;
            }
        }
    }

    protected override void Die()
    {
        gameManager.IncreaseCash(value);
        gameManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }
}
