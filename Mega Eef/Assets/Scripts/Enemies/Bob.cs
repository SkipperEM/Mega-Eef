using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : EnemyBase, ITakeDamage
{
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {

    }

}
