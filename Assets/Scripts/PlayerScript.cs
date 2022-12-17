using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MobileEntity
{
    [SerializeField] private GameManager gameManager;

    /* Movement fields */
    [SerializeField] private float YVel;
    [SerializeField] private float accl, baseSpd, jumpPwr, XSensitivity;
    private float fwdAccl, fwdSpd;

    [SerializeField] private float groundedFriction, aerialFriction;
    private Vector3 rotation = Vector3.zero;

    /* Shooting fields */
    [SerializeField] private GameObject icicleObj;
    private int iciclesLeft;

    /* Tower placing fields */
    [SerializeField] private GameObject[] towers;
    private GameObject chosenTower;

    bool compareOnGround;

    bool buildMode;

    void Start()
    {
        fwdAccl = accl;
        fwdSpd = baseSpd;
        chosenTower = towers[0];
        chosenTower.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        buildMode = !gameManager.waveInProgress;

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

        if (Input.GetKeyDown(KeyCode.K))
        {
            gameManager.StartNextWave();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (buildMode)
            {
                Debug.LogWarning("Tower placement not implemented");
                // TODO: something something set the tower down
            }
            else
            {
                Instantiate(icicleObj, cameraController.self.camTrfm.position + cameraController.self.camTrfm.forward, cameraController.self.camTrfm.rotation);
            }
        }

        rotation.y = Input.GetAxis("Mouse X") * XSensitivity;
        trfm.localEulerAngles += rotation;
    }

    void FixedUpdate()
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

        RaycastHit hit;

        if (buildMode && Physics.Raycast(this.transform.position, Camera.main.transform.forward, out hit, 20f))
        {
            chosenTower.SetActive(true);
            // FIXME: Pivot point of the tower should probably be the bottom of it such that placement is smoother
            chosenTower.transform.position = hit.point;
        }
        else
        {
            chosenTower.SetActive(false);
        }
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
