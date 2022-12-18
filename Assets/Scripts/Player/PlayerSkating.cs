using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkating : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rotation;
    Vector3 direction;
    float skatingSpeedUpModifier = 0.02f, skatingSlowDownModifier = 0.02f;

    float maxSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        direction = rb.velocity.normalized;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity += direction * skatingSpeedUpModifier;
        }

        if(rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = maxSpeed * direction;
        }

        rotation.y = Input.GetAxis("Mouse X") * 1;
        transform.localEulerAngles += rotation;
    }
}
