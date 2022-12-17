using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorHandler : MonoBehaviour
{
    Animator anim;
    PlayerShooting shootScript;

    private void Start()
    {
        anim = GetComponent<Animator>();
        shootScript = GetComponentInParent<PlayerShooting>();
    }

    // Animator callbacks
    public void ResetAtk()
    {
        anim.SetBool("Attack", false);
    }

    public void ActivateDrink()
    {
        shootScript.DrinkSetActive(true);
    }

    public void DeactivateDrink()
    {
        shootScript.DrinkSetActive(false);
    }
}
