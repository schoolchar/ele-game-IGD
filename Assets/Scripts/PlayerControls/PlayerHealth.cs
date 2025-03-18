using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //Controls player XP, health, and upgrades that affect xp and health

    public int health;
    public int xp;
    public bool ifPlayerAlive;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI xpText;
    

    [Header("Upgrades - XP")]
    public bool xpMod;
    public int xpModVal;

    [Header("Ungrades - Trapeze")]
    public PlayerMovement playerMovement;

    private void Start()
    {
        InitValues();
        ifPlayerAlive = true;
    }

    private void OnCollisionEnter(Collision _other)
    {
        LoseHealth(_other);
    }


    void InitValues()
    {
        //Change later to not use tags
        hpText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
        xpText = GameObject.FindGameObjectWithTag("XPText").GetComponent<TextMeshProUGUI>();

        hpText.text = "Health = " + health;
        xpText.text = "XP = " + xp;
    }


    /// <summary>
    /// Decrease player health by 1 when it collides with an enemy
    /// </summary>
    void LoseHealth(Collision other)
    {
        //Check for enemy
        if(other.gameObject.tag == "Enemy")
        {
            //Decrement health
            if (health > 0)
            {
                health--;
                hpText.text = "Health = " + health.ToString();
            }

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

    /*void AddXP(int xpGain)
    {
        if(xpMod)
        {
            xpGain *= xpModVal;
            xpText.text = "XP = " + xp.ToString();
        }

        xp += xpGain;
    }*/


    
}
