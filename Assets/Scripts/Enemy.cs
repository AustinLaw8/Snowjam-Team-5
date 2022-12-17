using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MobileEntity
{
    private static float I_AM_HERE_THRESHHOLD = .1f;

    [SerializeField] protected float health;
    
    // GameManager will probably pass every enemy the nodes
    protected Queue<Vector3> movementNodes;

    void Start()
    {
        if (movementNodes == null || movementNodes.Count == 0)
        {
            Debug.LogError("Enemy instantiated with no pathing");
            Destroy(this.gameObject);
        }
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
                movementNodes.Dequeue();
                if (movementNodes.Count == 0)
                {
                    // If popping pops the last node, it means we are at the end
                    // Do something w/ GameManager or Player and decrement health or something
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
        this.transform.LookAt(nextNode - this.transform.position);
        
        // This might not work properly, but walk towards next node
        addHorizontalVelocity(1,0,1,0);
    }
}
