using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEntity : MonoBehaviour
{
    [SerializeField] protected Transform trfm;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected onGroundScript m_onGroundScript;
    // Start is called before the first frame update
    [SerializeField] protected float velocityModifier = 1f, friction;
    float targetX, targetZ, maxX, maxZ;
    Vector3 vect3, vect2;

    [SerializeField] bool disconnectedTrfms;

    void Awake()
    {
        if (!disconnectedTrfms)
        {
            trfm = this.transform;
            rb = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    protected void addHorizontalVelocity(float forwardAmount, float rightwardAmount, float forwardMax, float rightwardMax)
    {
        forwardAmount *= velocityModifier;
        rightwardAmount *= velocityModifier;
        forwardMax *= velocityModifier;
        rightwardMax *= velocityModifier;

        targetX = trfm.forward.x * forwardAmount + trfm.right.x * rightwardAmount; //attempted X velocity difference
        targetZ = trfm.forward.z * forwardAmount + trfm.right.z * rightwardAmount; //attempted Z velocity difference

        maxX = trfm.forward.x * forwardMax + trfm.right.x * rightwardMax; //max X velocity difference
        maxZ = trfm.forward.z * forwardMax + trfm.right.z * rightwardMax; //max Z velocity difference

        if (targetX > 0) //if intent to accelerate towards pos X
        {
            if (rb.velocity.x + targetX < maxX)
            {
                vect3.x = targetX;
            }
            else
            {
                if (maxX - rb.velocity.x < 0)
                {
                    vect3.x = 0;
                }
                else
                {
                    vect3.x = maxX - rb.velocity.x;
                }
            }
        }
        else //if intent to accelerate towards neg X
        {
            if (rb.velocity.x + targetX > maxX)
            {
                vect3.x = targetX;
            }
            else
            {
                if (maxX - rb.velocity.x > 0)
                {
                    vect3.x = 0;
                }
                else
                {
                    vect3.x = maxX - rb.velocity.x;
                }
            }
        }

        if (targetZ > 0) //if intent to accelerate towards pos Z
        {
            if (rb.velocity.z + targetZ < maxZ)
            {
                vect3.z = targetZ;
            }
            else
            {
                if (maxZ - rb.velocity.z < 0)
                {
                    vect3.z = 0;
                }
                else
                {
                    vect3.z = maxZ - rb.velocity.z;
                }
            }
        }
        else //if intent to accelerate towards neg Z
        {
            if (rb.velocity.z + targetZ > maxZ)
            {
                vect3.z = targetZ;
            }
            else
            {
                if (maxZ - rb.velocity.z > 0)
                {
                    vect3.z = 0;
                }
                else
                {
                    vect3.z = maxZ - rb.velocity.z;
                }
            }
        }

        vect3.y = 0;
        rb.velocity += vect3;
    }

    protected void setYVelocity(float value)
    {
        vect3.y = value;

        vect3.x = rb.velocity.x;
        vect3.z = rb.velocity.z;

        rb.velocity = vect3;
    }
    protected void addYVelocity(float amount, float max)
    {
        vect3.x = 0;
        vect3.z = 0;

        if (rb.velocity.y + amount > max)
        {
            vect3.y = max - rb.velocity.y;
        }
        else
        {
            vect3.y = amount;
        }

        rb.velocity += vect3;
    }

    [SerializeField] float ratio, magnitude;
    protected void applyHorizontalFriction(float amount)
    {
        if (Mathf.Abs(rb.velocity.x) > 0.0001f && Mathf.Abs(rb.velocity.z) > 0.0001f)
        {
            vect2.x = rb.velocity.x;
            vect2.y = rb.velocity.z;
            magnitude = vect2.magnitude;
            ratio = (magnitude - amount) / magnitude;

            if (ratio > 0)
            {
                vect3.x = rb.velocity.x * ratio;
                vect3.z = rb.velocity.z * ratio;
                vect3.y = rb.velocity.y;

                rb.velocity = vect3;
            }
            else
            {
                vect3.x = 0;
                vect3.y = rb.velocity.y;
                vect3.z = 0;
                rb.velocity = vect3;
            }
        }
        else
        {
            vect3.x = 0;
            vect3.y = rb.velocity.y;
            vect3.z = 0;
            rb.velocity = vect3;
        }
    }

    protected void applyHorizontalDrag(float amount)
    {
        vect3.x = rb.velocity.x;
        vect3.z = rb.velocity.z;

        vect3 *= amount;
        vect3.y = rb.velocity.y;

        rb.velocity = vect3;
    }

    public void setVelocity(Vector3 vect3)
    {
        rb.velocity = vect3;
    }
}
