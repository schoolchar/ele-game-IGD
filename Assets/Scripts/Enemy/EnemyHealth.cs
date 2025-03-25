using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health, maxHealth;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;
    [SerializeField] FloatingHealthbar healthbar;

    private void Awake()
    {
        healthbar = GetComponent<FloatingHealthbar>();
    }

    private void Start()
    {
        healthbar.UpdateHealthBar(health, maxHealth);
    }

    public void TakeDamage(float _damage = 0)
    {
        healthbar.UpdateHealthBar(health, maxHealth);

        if (_damage == 0)
        {
            health -= enemyTakeDamage;
        }
        else
        {
            health -= _damage;
        }

        CheckForDeath();
    }

    private void CheckForDeath()
    {
        if (health <= 0)
        {
            FindAnyObjectByType<PlayerHealth>().AddXP(xpOnDeath);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage();

        if (_other.gameObject.tag == "Projectile")
        {
            Destroy(_other.gameObject);
        }
    }
}