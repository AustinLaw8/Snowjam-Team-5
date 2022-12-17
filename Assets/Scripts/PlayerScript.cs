using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MobileEntity
{
    [SerializeField] float YVel;

    [SerializeField] float accl, baseSpd, jumpPwr, XSensitivity;
    float fwdAccl, fwdSpd;
    [SerializeField] float groundedFriction, aerialFriction;
    Vector3 rotation = Vector3.zero;

    bool compareOnGround;

    void Start()
    {
        fwdAccl = accl;
        fwdSpd = baseSpd;
    }

    // Update is called once per frame
    void Update()
    {

        YVel = rb.velocity.y;

        if (Input.GetKeyDown(KeyCode.Space) && m_onGroundScript.isOnGround())
        {
            addYVelocity(jumpPwr, jumpPwr * 3);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            fwdSpd = baseSpd * 1.6f;
            //fwdAccl = accl * 1.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            fwdSpd = baseSpd;
            fwdAccl = accl;
        }


        rotation.y = Input.GetAxis("Mouse X") * XSensitivity;
        trfm.localEulerAngles += rotation;
    }

    private void FixedUpdate()
    {
        applyHorizontalFriction(friction);
        if (compareOnGround != m_onGroundScript.isOnGround())
        {
            compareOnGround = m_onGroundScript.isOnGround();
            if (compareOnGround)
            {
                friction = groundedFriction;
                velocityModifier = 1f;
            }
            else
            {
                friction = aerialFriction;
                velocityModifier = .5f;
            }
        }
        processHorizontalInput();
    }

    bool noInputFlag;
    void processHorizontalInput()
    {
        noInputFlag = false;
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                addHorizontalVelocity(fwdAccl * .707f, -accl * .707f, fwdSpd * .707f, -baseSpd * .707f);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                addHorizontalVelocity(fwdAccl * .707f, accl * .707f, fwdSpd * .707f, baseSpd * .707f);
            }
            else
            {
                addHorizontalVelocity(fwdAccl, 0, fwdSpd, 0);
            }
        }
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                addHorizontalVelocity(-accl * .707f, -accl * .707f, -baseSpd * .707f, -baseSpd * .707f);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                addHorizontalVelocity(-accl * .707f, accl * .707f, -baseSpd * .707f, baseSpd * .707f);
            }
            else
            {
                addHorizontalVelocity(-accl, 0, -baseSpd, 0);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                addHorizontalVelocity(0, -accl, 0, -baseSpd);
            }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
            {
                addHorizontalVelocity(0, accl, 0, baseSpd);
            }
            else
            {
                noInputFlag = true;
            }
        }
    }
}
