using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Controls player XP, health, and upgrades that affect xp and health



    public int health;
    public int xp;


    [Header("Upgrades - XP")]
    public bool xpMod;
    public int xpModVal;

    [Header("Ungrades - Trapeze")]
    public PlayerMovement playerMovement;
    private void OnTriggerEnter(Collider other)
    {
        LoseHealth(other);
    }

    /// <summary>
    /// Decrease player health by 1 when it collides with an enemy
    /// </summary>
    void LoseHealth(Collider _other)
    {
        //Check for enemy
        if(_other.gameObject.tag == "Enemy")
        {
            //Decrement health
            health--;

            //Check if player is at 0 health
            CheckForDeath();
        }
    } //END LoseHealth()

    /// <summary>
    /// 
    /// </summary>
    void CheckForDeath()
    {
        //Check if player is at 0 health
        if(health <= 0)
        {
            //Run death code
            Debug.Log("Player dies");
        }
        
    } //END CheckForDeath()

    void AddXP(int xpGain)
    {
        if(xpMod)
        {
            xpGain *= xpModVal;
        }

        xp += xpGain;
    }


    
}
