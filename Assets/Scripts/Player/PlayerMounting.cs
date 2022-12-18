using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMounting : MonoBehaviour
{
    MountableTower curTower;
    GameObject cam;
    PlayerMovementController mainControl;
    Vector3 originalPos;
    Quaternion originalRot;
    // Start is called before the first frame update
    void Start()
    {
        mainControl = GetComponent<PlayerMovementController>();
        cam = mainControl.cam.gameObject;
    }   

    private void OnEnable()
    {
        if (mainControl == null)
        {
            mainControl = GetComponent<PlayerMovementController>();
            cam = mainControl.cam.gameObject;
        }
        Mount(mainControl.inRangeTower);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        Dismount();
    }

    public void Mount(MountableTower tower)
    {
        curTower = tower;
        originalPos = cam.transform.position;
        originalRot = cam.transform.rotation;
        Debug.Log(originalPos);
        cam.transform.GetChild(0).gameObject.SetActive(false);  // Disable arms
    }

    // Update is called once per frame
    void Update()
    {
        if (curTower != null)  // Mounted
        {
            float horiRot = Input.GetAxis("Mouse X");
            float vertRot = Input.GetAxis("Mouse Y");
            curTower.xaxis += horiRot;
            curTower.yaxis -= vertRot;
            MoveCamera();
            if (Input.GetMouseButton(0))
            {
                curTower.Shoot();
            }
        }
    }

    void MoveCamera()
    {
        Transform trfmRef = curTower.camRef;
        cam.transform.position = Vector3.Lerp(cam.transform.position, trfmRef.position, 0.4f);
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, trfmRef.rotation, 0.4f);
        // Debug.Log(trfmRef.position);
        
    }

    public void Dismount()
    {
        curTower = null;
        cam.transform.position = originalPos;
        cam.transform.rotation = originalRot;
        cam.transform.GetChild(0).gameObject.SetActive(true);  // Enable arms
    }
}
