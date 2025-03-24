using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public int xpOnDeath;
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if(health <= 0)
        {
            FindAnyObjectByType<CharacterStats>().AddXP(xpOnDeath);
            Destroy(this.gameObject);
        }
    }
}
