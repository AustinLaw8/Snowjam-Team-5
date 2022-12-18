using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkating : MonoBehaviour
{
    Rigidbody rb;
    Vector3 rotation;
    Vector3 direction;
    [SerializeField] float skatingSpeedUpModifier = 0.03f, skatingSlowDownModifier = 0.02f;

    [SerializeField] float maxSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        if(rb)
        {
            Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            direction = horizontalVelocity.normalized;
        }

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
            rb.velocity -= direction * skatingSpeedUpModifier;
        }

        rotation.y = Input.GetAxis("Mouse X") * 1;
        transform.localEulerAngles += rotation;
    }
}
