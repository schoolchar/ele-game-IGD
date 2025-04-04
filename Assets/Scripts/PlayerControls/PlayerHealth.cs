using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerHealth : MonoBehaviour
{
    //Controls player XP, health, and upgrades that affect xp and health
    public int maxHealth;
    public int health;
    public int xp;
    public int money;
    public int level; //Reset every run

    public bool debugHealth;

    public ChooseWeapons chooseWeapons;
    public SaveData saveData;
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
        //InitValues();
    }

    private void OnCollisionEnter(Collision other)
    {
        LoseHealth(other);
    }


   public void InitValues()
    {
        if(debugHealth)
        {
            maxHealth = 200;
        }

        health = maxHealth;
        //Change later to not use tags
        hpText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
        xpText = GameObject.FindGameObjectWithTag("XPText").GetComponent<TextMeshProUGUI>();

        hpText.text = "Health = " + health;
        xpText.text = "XP = " + xp;

        chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        saveData = GetComponent<SaveData>();
    }


        
    


    /// <summary>
    /// Decrease player health by 1 when it collides with an enemy
    /// </summary>
    void LoseHealth(Collision _other)
    {
        //Check for enemy
        if(_other.gameObject.tag == "Enemy")
        {
            //Decrement health
            health--;
            hpText.text = "Health = " + health.ToString();

            //Check if player is at 0 health
            CheckForDeath();
        }
    } //END LoseHealth()

    /// <summary>
    /// Check if the player has dies
    /// </summary>
    void CheckForDeath()
    {
        //Check if player is at 0 health
        if (health <= 0)
        {
            //Disable weapons
            Debug.Log("Player dies");
            playerMovement.ringOfFire.SetActive(false);
            playerMovement.knifeThrow.hasKnife = false;
            playerMovement.knifeThrow.enabled = false;

            //Check if this is the highest level the player has reached
            string _path = Application.persistentDataPath + "/HighScore.txt";
            if(File.Exists(_path))
            {
                //get path, if it is the highest, override data in file
                int _highScore = int.Parse(File.ReadAllText(_path));
                if(level > _highScore)
                {
                    saveData.SaveHighScore(level);
                }
            }
            else
            {
                saveData.SaveHighScore(level);
            }


            //Reset level and load menu
            level = 0;
            SceneManager.LoadScene(0);
        }
        
    } //END CheckForDeath()

    //Add xp, called when enemies are killed
    public void AddXP(int xpGain)
    {
        //Check if the player has the xp upgrade
        if (xpMod)
        {
            xpGain *= xpModVal;
           
        }
        
        //UI update
        xp += xpGain;
        xpText.text = "XP = " + xp.ToString();

        //New weapon call
        chooseWeapons.ActivateMenu(xp);
    }


    
}
