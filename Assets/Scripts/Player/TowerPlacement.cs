using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
///     Handles hologram of towers,
///     placement of towers, and validation of placement
/// </summary>
public class TowerPlacement : MonoBehaviour
{
    private static float MAX_DIST = 20f;
    [SerializeField] private GameObject SPHERE;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private List<GameObject> towerPrefabs;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private TMP_Text UI_Text;

    // Indexes into towerPrefab
    private int index;

    // Clone of current towerPrefab[index]
    private GameObject chosenTower;
    private GameObject rangeIndicator;
    private Vector3 offset;

    void Start()
    {
        index = 0;
        if (UI_Text == null) UI_Text = GameObject.Find("TowerPlacementText").GetComponent<TMP_Text>();
        if (gameManager == null) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Handles actual placement on click, and rotate on Q and E
    void Update()
    {
        if (chosenTower.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (ValidateTowerLocation())
                {
                    PlaceTower();
                }
            }

            if (Input.GetKey(KeyCode.Q))
            {
                chosenTower.transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            } 
            else if (Input.GetKey(KeyCode.E))
            {
                chosenTower.transform.Rotate(0f, -rotationSpeed * Time.deltaTime, 0f);
            }
        }
        UpdateText();
    }

    void UpdateText()
    {
        UI_Text.enabled = chosenTower.activeSelf;
    }

    // TODO:
    bool ValidateTowerLocation()
    {
        return true;
    }

    void PlaceTower()
    {
        // TODO: some shader stuff idk
        if (gameManager.SpendCash(chosenTower.GetComponent<Tower>().GetCost()))
        {
            chosenTower.GetComponent<Collider>().enabled = true;
            chosenTower = CreateHologram();
            Destroy(rangeIndicator);
        }
    }

    // Handles hologram 
    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Camera.main.transform.forward, out hit, MAX_DIST))
        {
            // If hovering over a tower, do something else (allow for selling or something, but we figure that out later)
            if (hit.transform.gameObject.layer == 7)
            {
                // TODO: Create range indicator
                Debug.Log("Hovering over other tower");
                // hit.transform.GetChild(0).activeSelf = true;
                chosenTower.SetActive(false);
            }
            else
            {
                // Place hologram, allow rotation
                chosenTower.SetActive(true);

                chosenTower.transform.position = hit.point + offset;
            }
        }
        else
        {
            chosenTower.SetActive(false);
        }
    }


    GameObject CreateHologram()
    {
        GameObject hologram = GameObject.Instantiate(towerPrefabs[index]);
        offset = new Vector3(0f, hologram.GetComponent<Collider>().bounds.extents.y, 0f);
        hologram.GetComponent<Collider>().enabled = false;
        rangeIndicator = GameObject.Instantiate(SPHERE);
        float temp = hologram.GetComponent<Tower>().GetRange() * 2f;
        rangeIndicator.transform.localScale = new Vector3(temp, temp, temp);
        rangeIndicator.transform.SetParent(hologram.transform);
        UI_Text.enabled = true;
        UI_Text.text = $"Cost: {hologram.GetComponent<Tower>().GetCost()}";
        return hologram;
    }

    void OnEnable()
    {
        chosenTower = CreateHologram();
    }

    void OnDisable()
    {
        UI_Text.enabled = false;
        Destroy(chosenTower);
    }
}
