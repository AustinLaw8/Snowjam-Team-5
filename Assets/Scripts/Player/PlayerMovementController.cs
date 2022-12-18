using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    enum MoveState { Moving, Skating, Mounting};
    PlayerMovement movement;
    PlayerSkating skating;
    // add mounting script

    MoveState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = MoveState.Moving;
        movement = GetComponent<PlayerMovement>();
        skating = GetComponent<PlayerSkating>();
    }

    // Update is called once per frame
    void Update()
    {
        // skating toggle
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentState = MoveState.Skating;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentState = MoveState.Moving;
        }
        // add mounting toggle in future

        switch(currentState)
        {
            case MoveState.Moving:
                movement.enabled = true;
                skating.enabled = false;
                break;
            case MoveState.Skating:
                movement.enabled = false;
                skating.enabled = true;
                break;
        }
    }
}
