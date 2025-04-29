using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private AudioSource[] audioSources;
    public float health;
    public float maxHealth;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;

    private MoneyDrop moneyDrop;

    private void Start()
    {
        audioSources = GetComponents<AudioSource>();
        health = maxHealth;
        moneyDrop = GetComponent<MoneyDrop>();
    }

    /// <summary>
    /// Decrease health on enemy
    /// </summary>
    public void TakeDamage(float _damage = 0)
    {
        //Check for default value if no damage value is passed
        if (_damage == 0)
        {
            health -= enemyTakeDamage;
            audioSources[1].Play();
        }
        else
        {
            //Decrease passed damage
            health -= _damage;
            audioSources[1].Play();
        }
        if (health <= 0)
        {
            //Check for enemy death, add xp for death
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
            //Drop coins on death
            moneyDrop.DropCoins();
           // Destroy(this.gameObject);
        }
    } //END TakeDamage()

    private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage();

        if (_other.gameObject.tag == "Projectile")
        {
            Destroy(_other.gameObject);
        }
    }
}