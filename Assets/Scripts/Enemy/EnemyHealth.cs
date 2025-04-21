using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public int xpOnDeath;
    public float enemyTakeDamage = 2f;

    private MoneyDrop moneyDrop;

    private void Start()
    {
        health = maxHealth;
        moneyDrop = GetComponent<MoneyDrop>();
    }

    public void TakeDamage(float _damage = 0)
    {
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