using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Handles hologram of towers,
///     placement of towers, and validation of placement
/// </summary>
public class TowerPlacement : MonoBehaviour
{
    [SerializeField] private List<GameObject> towerPrefabs;

    // Indexes into towerPrefab
    private int index;

    // Clone of current towerPrefab[index]
    private GameObject chosenTower;

    void Start()
    {
        index = 0;
    }

    // Handles actual placement on click
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning("Tower placement not implemented");
            // TODO: something something set the tower down
        }
    }

    // Handles hologram 
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Camera.main.transform.forward, out hit, 20f))
        {
            chosenTower.SetActive(true);
            // FIXME: Pivot point of the tower should probably be the bottom of it such that placement is smoother
            // TODO: Hologram shader
            chosenTower.transform.position = hit.point;
        }
        else
        {
            chosenTower.SetActive(false);
        }
    }

    void OnEnable()
    {
        chosenTower = GameObject.Instantiate(towerPrefabs[index]);
        chosenTower.GetComponent<Collider>().enabled = false;    
    }

    void OnDisable()
    {
        Destroy(chosenTower);
    }
}
