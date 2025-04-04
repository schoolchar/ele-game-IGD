using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //Controls player XP, health, and upgrades that affect xp and health

    public int maxHealth; //Maximum amount of health the player can have at any point
    public int health;
    public int xp;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI xpText;


    [Header("Upgrades - XP")]
    public bool xpMod;
    public int xpModVal;

    //[Header("Ungrades - Trapeze")]
    //public PlayerMovement playerMovement;

    private void Start()
    {
        InitValues();
    }

    private void OnCollisionEnter(Collision other)
    {
        LoseHealth(other);
    }


    void InitValues()
    {
        //Change later to not use tags

        //Debug if statememt
        if (hpText != null && xpText != null)
        {
            //hpText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
            //xpText = GameObject.FindGameObjectWithTag("XPText").GetComponent<TextMeshProUGUI>();

            hpText.text = "Health = " + health;
            xpText.text = "XP = " + xp;
        }
    }


    /// <summary>
    /// Decrease player health by 1 when it collides with an enemy
    /// </summary>
    void LoseHealth(Collision _other)
    {
        //Check for enemy
        if (_other.gameObject.tag == "Enemy" && hpText != null)
        {
            //Decrement health
            health--;
            hpText.text = "Health = " + health.ToString();

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
        if (health <= 0)
        {
            //Run death code
            Debug.Log("Player dies");
        }

    } //END CheckForDeath()

    void AddXP(int xpGain)
    {
        if (xpMod)
        {
            xpGain *= xpModVal;
            xpText.text = "XP = " + xp.ToString();
        }

        xp += xpGain;
    }
}