using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    private AudioSource[] audioSources;
    public float health;
    public float maxHealth;

    //public Slider slider;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;

    private MoneyDrop moneyDrop;
    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        health = maxHealth;
        moneyDrop = GetComponent<MoneyDrop>();
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        //slider.value = currentValue / maxValue;
    }


    public void TakeDamage(float _damage = 0)
    {
        //if(slider != null) 
       // UpdateHealthBar(health, maxHealth);

        if (_damage == 0)
        {
            health -= enemyTakeDamage;
        }
        else
        {
            health -= _damage;
            audioSources[1].Play();
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

    private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage();

        if (_other.gameObject.tag == "Projectile")
        {
            Destroy(_other.gameObject);
        }
    }
}