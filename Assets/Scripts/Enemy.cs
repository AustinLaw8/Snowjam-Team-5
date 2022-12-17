using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MobileEntity
{
    private static float I_AM_HERE_THRESHHOLD = .3f;

    [SerializeField] protected int hp;
    
    // GameManager will probably pass every enemy the nodes
    protected Queue<Vector3> movementNodes;
    protected GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movementNodes = new Queue<Vector3>(gameManager.tempNodes);
    }

    void FixedUpdate()
    {
        Move();
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
                    // If popping pops the last node, it means we are at the end
                    Die();
                    Debug.Log("End reached");
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
        addHorizontalVelocity(1,0,1,0);
    }
    
    protected override void Die()
    {
        gameManager.RemoveEnemy(this);
        Destroy(this.gameObject);
    }

    public void SetNodes(Vector3[] nodes) { movementNodes = new Queue<Vector3>(nodes); }
}
