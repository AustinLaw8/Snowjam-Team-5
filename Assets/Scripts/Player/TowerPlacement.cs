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
    [SerializeField] private float scrollRate=.1f;

    [SerializeField] private TMP_Text UI_Text;

    // Indexes into towerPrefab
    public float index;

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
        GetSelectedTower();
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

    // Handles hologram 
    void FixedUpdate()
    {

        RaycastHit hit;

        if (Physics.Raycast(this.transform.position, Camera.main.transform.forward, out hit, MAX_DIST))
        {
            // If hovering over a tower, display range for that tower
            if (hit.transform.gameObject.layer == 7)
            {
                Debug.Log("Hovering over other tower");
                AttachRangeIndicator(hit.transform);
                chosenTower.SetActive(false);
            }
            else
            {
                // Place hologram, allow rotation
                AttachRangeIndicator(chosenTower.transform);
                chosenTower.transform.position = hit.point + offset;

                MeshRenderer[] meshes = chosenTower.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mesh in meshes)
                {
                    if(ValidateTowerLocation())
                    {
                        mesh.material.SetColor("_Color", new Color(0f, 1f, 0f, .5f));
                    } else
                    {
                        mesh.material.SetColor("_Color", new Color(1f, 0f, 0f, .5f));
                    }
                }
                chosenTower.SetActive(true);
            }
        }
        else
        {
            chosenTower.SetActive(false);
        }
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

    void GetSelectedTower()
    {
        int last = Mathf.RoundToInt(index);

        // Get index by scroll wheel
        if(Mathf.Abs(Input.mouseScrollDelta.y) != 0)
        {
            index += Input.mouseScrollDelta.y * scrollRate;
            Debug.Log(index);
        }

        // Get index by key press
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            index = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            index = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            index = 3;
        }

        int tempInd = (Mathf.RoundToInt(index) + towerPrefabs.Count) % towerPrefabs.Count;
        if (last != tempInd)
        {
            Debug.Log($"Switching tower from {last} to {index}");
            index = tempInd;
            GameObject temp = chosenTower;
            chosenTower = CreateHologram();
            Destroy(temp);
        }
    }

    void UpdateText()
    {
        UI_Text.enabled = chosenTower.activeSelf;
    }

    // TODO:
    bool ValidateTowerLocation()
    {
        // Debug.Log(chosenTower.transform.position);
        
        //Physics.BoxCast(chosenTower.transform.position + Vector3.up * .75f, Vector3.one, Vector3.down, out var targ, Quaternion.identity, 1f, 1 << 6);
        //if (targ.collider.CompareTag("Navmesh"))
        //{
        //    Debug.Log("Found");
        //    return false;
        //} else
        //{
        //    return true;
        //    Debug.Log("Not found");
        //}


        foreach (RaycastHit hit in Physics.BoxCastAll(chosenTower.transform.position + Vector3.up * .75f, Vector3.one, Vector3.down, Quaternion.identity, 0.7f, 1 << 6))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Navmesh"))
            {
                Debug.Log("Found");
                return false;
            } else
            {
                Debug.Log("Not found");
                return true;
            }
              
        }
        Debug.Log("Not found");
        return true;
    }

    void PlaceTower()
    {
        // TODO: some shader stuff idk
        if (gameManager.SpendCash(chosenTower.GetComponent<Tower>().GetCost()))
        {
            Destroy(rangeIndicator);
            GameObject tower = Instantiate(towerPrefabs[Mathf.RoundToInt(index)], chosenTower.transform);
            tower.transform.parent = null;
            Destroy(chosenTower);
            //chosenTower.GetComponent<Collider>().enabled = true;
            chosenTower = CreateHologram();
        }
    }

    GameObject CreateHologram()
    {
        // Creates a hologram by cloning a Prefab,
        GameObject hologram = GameObject.Instantiate(towerPrefabs[Mathf.RoundToInt(index)]);

        offset = new Vector3(0f, -hologram.GetComponent<Collider>().bounds.min.y, 0f);

        // Attaching to it the range indicator
        hologram.GetComponent<Collider>().enabled = false;
        rangeIndicator = GameObject.Instantiate(SPHERE);
        AttachRangeIndicator(hologram.transform);

        // And enabling the UI text
        UI_Text.text = $"Cost: {hologram.GetComponent<Tower>().GetCost()}";
        hologram.SetActive(false);

        //making it translucent
        MeshRenderer[] meshes = hologram.GetComponentsInChildren<MeshRenderer>();
        Material mat = new Material(Shader.Find("Standard"));
        foreach (MeshRenderer mesh in meshes)
        {
            mat = mesh.material;
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            mesh.material = mat;
        }

        //rotating it to face away from player
        //hologram.transform.rotation = Quaternion.LookRotation(Camera.main.transform.rotation, Vector3.up);
        Vector3 eulerRotation = Camera.main.transform.rotation.eulerAngles;
       hologram.transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
        return hologram;
    }

    void AttachRangeIndicator(Transform transform)
    {
        float temp = transform.GetComponent<Tower>().GetRange() * 2f;
        rangeIndicator.transform.localScale = new Vector3(temp, temp, temp);
        rangeIndicator.transform.localPosition = Vector3.zero;
        rangeIndicator.transform.SetParent(transform);
    }
}
