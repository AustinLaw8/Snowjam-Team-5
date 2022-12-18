using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class MountableTower : Tower
{
    public float xaxis = 0;
    public float yaxis = 0;
    [SerializeField] public Transform camRef;

    protected override bool AttemptAttack() { return false; }
    
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
