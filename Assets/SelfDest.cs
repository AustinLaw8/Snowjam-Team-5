using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDest : MonoBehaviour
{
    [SerializeField] float duration;
    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
