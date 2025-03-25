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
        
            FindAnyObjectByType<PlayerHealth>().AddXP(xpOnDeath);
            Destroy(this.gameObject);
        
    }

    private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage(_other);
    }
}
