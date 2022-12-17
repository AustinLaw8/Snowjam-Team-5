using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float movespeed;
    
    protected Queue<Vector3> movementNodes;

    void Start()
    {
        if (movementNodes == null || movementNodes.Count == 0)
        {
            Debug.LogError("Enemy instantiated with no pathing");
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Move();

        // if at end { player health go down or something }
    }
    
    void Move()
    {
        Vector3 nextNode = movementNodes.Peek();
        Vector3 dir = nextNode - this.transform.position;

        // something about animating here idk ive never worked in 3d 
    }
}
