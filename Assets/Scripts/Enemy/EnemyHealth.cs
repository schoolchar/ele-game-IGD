using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    //public Slider slider;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;

    private void Start()
    {
        health = maxHealth;
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        //slider.value = currentValue / maxValue;
    }


    public void TakeDamage(float _damage = 0)
    {
        UpdateHealthBar(health, maxHealth);

        if (_damage == 0)
        {
            health -= enemyTakeDamage;
        }
        else
        {
            health -= _damage;
        }
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