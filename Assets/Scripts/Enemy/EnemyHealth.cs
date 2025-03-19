using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private float damageTake = 2f;

    public void TakeDamage()
    {
        health -= damageTake;
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
