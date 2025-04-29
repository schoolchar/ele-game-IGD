using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //REPLACED BY PLAYERHEALTH.CS

    public int xp;
    //public Stat damage;
    //public Stat armor;
    public int maxHealth = 100;

    [Header("Upgrades - XP")]
    public bool xpMod;
    public int xpModVal;

    [Header("Ungrades - Trapeze")]
    public PlayerMovement playerMovement;

    public int currentHealth { get; private set;}

    private void Awake()
    {
        currentHealth = maxHealth;
    }

        private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Enemy")
        {
            //Decrement health
            TakeDamage(10);
        }
    }

    //Decrease player health
    public void TakeDamage(int damage)
    {
        //damage -= armor.GetValue();

        currentHealth -= damage;
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if(currentHealth <= 0)
        {
            CheckForDeath();

        }
    }

    void CheckForDeath()
    {
        //Check if player is at 0 health
        if (currentHealth <= 0)
        {
            //Run death code
            //Debug.Log("Player dies");
        }

    }

    public void AddXP(int xpGain)
    {
        if (xpMod)
        {
            xpGain *= xpModVal;
        }

        xp += xpGain;
    }
}
