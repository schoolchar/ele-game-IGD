using System;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public Slider slider;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;
    public int minDmg = -1;
    public int maxDmg = 1;

    private MoneyDrop moneyDrop;
    private void Start()
    {
        health = maxHealth;
        moneyDrop = GetComponent<MoneyDrop>();
        DamageScaling();
        HealthScaling();
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }


    public void TakeDamage(float _damage = 0)
    {
        int additive = Random.Range(minDmg, maxDmg);
        if(slider != null) 
        UpdateHealthBar(health, maxHealth);


        if (_damage == 0)
        {
            health -= enemyTakeDamage;
        }
        else
        {
            health -= _damage + additive;
        }
        if (health <= 0)
        {
            PlayerHealth _playerHealth = FindAnyObjectByType<PlayerHealth>();
            _playerHealth.AddXP(xpOnDeath);

            //Check if player has the life force upgrade
            if(_playerHealth.lifeForce)
            {
                //Add health when enemy dies
                _playerHealth.health += _playerHealth.healthPerEnemy;
                //Check if this puts the player over their max health
                if(_playerHealth.health > maxHealth)
                {
                    _playerHealth.health = _playerHealth.maxHealth;
                }
            }
            moneyDrop.DropCoins();
           // Destroy(this.gameObject);
        }
    }

    private void DamageScaling()
    {
        float timer = 0f;
        timer += Time.deltaTime;
        Debug.Log("Player damage scaled");
        if ((int)timer % 30 == 0)
        {
            maxDmg += 2;
            minDmg += 2;
        }
    }

    private void HealthScaling()
    {
        float timer = 0f;
        timer += Time.deltaTime;
        Debug.Log("Enemy health scaled");
        if ((int)timer % 30 == 0)
        {
            maxHealth += 3;
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