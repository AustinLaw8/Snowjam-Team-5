using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BasicTowerProjectile : MonoBehaviour
{
    private static float CLOSE_ENOUGH = .1f;

    [SerializeField] private float velocity;
    private Enemy target;
    private int damage;

    public void Initialize(Vector3 position, Enemy target, int damage)
    {
        this.transform.position = position;
        this.target = target;
        this.damage = damage;
    }

    void FixedUpdate()
    {
        this.transform.LookAt(target.transform.position);
        this.transform.Rotate(90, 0, 0);
        Vector3 dir = (target.transform.position - this.transform.position).normalized;
        this.transform.position += dir * velocity * Time.fixedDeltaTime;
        if (Vector3.Distance(target.transform.position, this.transform.position) <= CLOSE_ENOUGH)
        {
            target.damage(damage);
            Destroy(this.gameObject);
        }
    }

}
