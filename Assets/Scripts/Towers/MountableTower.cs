using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MountableTower : MonoBehaviour
{
    public float xaxis = 0;
    public float yaxis = 0;
    [SerializeField] public Transform camRef;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            PlayerMovementController player = other.gameObject.GetComponent<PlayerMovementController>();
            player.SetMountableTower(this);
        } catch {}
    }

    private void OnTriggerExit(Collider other)
    {
        try
        {
            PlayerMovementController player = other.gameObject.GetComponent<PlayerMovementController>();
            player.ResetMountableTower();
        }
        catch {}
    }

    public abstract void Shoot();
}
