using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] public Transform camTrfm;
    [SerializeField] float YSensitivity;
    public static cameraController self;
    Vector3 rotation;
    void Start()
    {
        self = GetComponent<cameraController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x = Input.GetAxis("Mouse Y") * YSensitivity;
        camTrfm.localEulerAngles -= rotation;
    }
}
