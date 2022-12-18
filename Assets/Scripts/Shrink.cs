using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    [SerializeField] Transform trfm;
    int tmr;

    private void FixedUpdate()
    {
        tmr++;
        if (tmr > 49)
        {
            trfm.localScale -= Vector3.one;
        }
    }
}
