using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float enemyTakeDamage = 2f;

    public void TakeDamage()
    {
        health -= enemyTakeDamage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        TakeDamage();
    }
}
