using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNavigation : MonoBehaviour
{
    public Transform goal;
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGoal();
    }

    public float GetPathDistance()
    {
        return agent.remainingDistance;
    }

    void CheckGoal()
    {
        if (agent.remainingDistance < 0.02f)
        {
            // Call to GameManager
        }
    }
}
