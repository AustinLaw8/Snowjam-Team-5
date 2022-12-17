using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEntity : MonoBehaviour
{
    [SerializeField] protected int HP, maxHP;
    public int entityID; //ID of entity (used to ignore self-damage sources)
    const int enemyID = 0, playerID = 1;
    
    void Start()
    {
        HP = maxHP;
    }

    public void TakeDmg(int dmg, int ignoreID = -1)
    {
        if (entityID == ignoreID) { return; }

        HP -= dmg;
        if (HP <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(int amount, bool allowOverheal)
    {
        HP += amount;
        if (HP > maxHP && !allowOverheal)
        {
            HP = maxHP;
        }
    }
}
