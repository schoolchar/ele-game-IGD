using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat armor;
    public int maxHealth = 100;
    public int currentHealth { get; private set;}

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        LoseHealth(other);
    }

    /// <summary>
    /// Decrease player health by 1 when it collides with an enemy
    /// </summary>
    void LoseHealth(Collider _other, int damage)
    {
        //Check for enemy
        if (_other.gameObject.tag == "Enemy")
        {
            //Decrement health
            currentHealth -= damage;

            //Check if player is at 0 health
            CheckForDeath();
        }
    } //END LoseHealth()

    void CheckForDeath()
    {
        //Check if player is at 0 health
        if (currentHealth <= 0)
        {
            //Run death code
            Debug.Log("Player dies");
        }

    } //END CheckForDeath()

}
