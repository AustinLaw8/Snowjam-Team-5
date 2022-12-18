using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] Transform trfm;
    [SerializeField] Vector3 rotation;

    // Update is called once per frame
    void FixedUpdate()
    {
        //trfm.Rotate(trfm.right * rate);
        trfm.localEulerAngles += rotation;
    }
}
