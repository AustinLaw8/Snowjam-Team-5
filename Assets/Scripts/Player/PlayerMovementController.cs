using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public cameraController cam;
    enum MoveState { Moving, Skating, Mounting};
    PlayerMovement movement;
    PlayerSkating skating;
    PlayerShooting shooting;
    PlayerMounting mounting;

    TowerPlacement building;
    public MountableTower inRangeTower;

    [SerializeField] MoveState currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = MoveState.Moving;
        movement = GetComponent<PlayerMovement>();
        skating = GetComponent<PlayerSkating>();
        mounting = GetComponent<PlayerMounting>();
        shooting = GetComponent<PlayerShooting>();
        building = GetComponent<TowerPlacement>();
        if (gameManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != MoveState.Mounting)
        {
            // skating toggle
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(movement.OnGround())
                {
                    currentState = MoveState.Skating;
                }

            }
            if (Input.GetKeyUp(KeyCode.LeftShift) && currentState == MoveState.Skating)
            {
                currentState = MoveState.Moving;
            }

            if (Input.GetKeyDown(KeyCode.F) && inRangeTower != null)
            {
                currentState = MoveState.Mounting;
            }
        } else
        {
            // mounting toggle
            if (Input.GetKeyDown(KeyCode.F))
            {
                currentState = MoveState.Moving;
            }
        }

        if (gameManager.paused)
        {
            movement.enabled = false;
            skating.enabled = false;
            mounting.enabled = false;
            cam.enabled = false;
            shooting.enabled = false;
            building.enabled = false;
        }
        else
        {
            switch(currentState)
            {
                case MoveState.Moving:
                    movement.enabled = true;
                    skating.enabled = false;
                    mounting.enabled = false;
                    cam.enabled = true;
                    shooting.enabled = gameManager.GetGameState() == GameState.Defending;
                    building.enabled = gameManager.GetGameState() == GameState.Building;
                    break;
                case MoveState.Skating:
                    movement.enabled = false;
                    skating.enabled = true;
                    mounting.enabled = false;
                    cam.enabled = true;
                    shooting.enabled = gameManager.GetGameState() == GameState.Defending;
                    building.enabled = gameManager.GetGameState() == GameState.Building;
                    break;
                case MoveState.Mounting:
                    movement.enabled = false;
                    skating.enabled = false;
                    mounting.enabled = true;
                    cam.enabled = false;
                    shooting.enabled = false;
                    building.enabled = false;
                    break;
            }
        }
    }

    public void SetMountableTower(MountableTower tower)
    {
        if (inRangeTower == null)
            inRangeTower = tower;
        Debug.Log("Mountable!");
    }

    public void ResetMountableTower()
    {
        inRangeTower = null;
        Debug.Log("Out of range");
    }
}
