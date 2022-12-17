using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGroundScript : MonoBehaviour
{
    [SerializeField] bool onGround;
    public bool isOnGround()
    {
        return onGround;
    }

    private void OnTriggerStay(Collider other)
    {
        onGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        onGround = false;
    }
}
