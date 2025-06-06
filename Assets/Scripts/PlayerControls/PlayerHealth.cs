using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using Unity.VisualScripting;

public class PlayerHealth : MonoBehaviour
{
    public delegate void OnPlayerDeath();
    public static event OnPlayerDeath onPlayerDeath;


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

    [Header("Upgrades - Life force")]
    public bool lifeForce;
    public int healthPerEnemy;
    public AudioSource damageSound;

    private void Start()
    {
        damageSound = GetComponent<AudioSource>();
        InitOnStartOnly();
        //InitValues();
    }

    private void OnCollisionEnter(Collision other)
    {
        LoseHealth(other);
    }

    private void InitOnStartOnly()
    {
        if(saveData.xp.level > 0)
        {
            xpMod = true;
            xpModVal = saveData.xp.affectOnXP;
        }

        if (saveData.health.level > 0)
        {
            maxHealth += saveData.health.affectOnHealth;
        }
    }

    /// <summary>
    /// Initialize all variables that need to be set on start
    /// </summary>
   public void InitValues()
    {
        if(debugHealth)
        {
            maxHealth = 50;
        }

        health = maxHealth;
        saveData = GetComponent<SaveData>();


        if (GameObject.FindGameObjectWithTag("HPText") != null)
        {
            //Change later to not use tags
            hpText = GameObject.FindGameObjectWithTag("HPText").GetComponent<TextMeshProUGUI>();
            xpText = GameObject.FindGameObjectWithTag("XPText").GetComponent<TextMeshProUGUI>();

            hpText.text = "Health = " + health;
            xpText.text = "XP = " + xp;

            chooseWeapons = FindAnyObjectByType<ChooseWeapons>();
        }

        //Check if any weapon objects were left active from previous round
        GameObject _hammer = GameObject.FindGameObjectWithTag("Hammer");
        LargeHammer _hammerS = GetComponentInChildren<LargeHammer>();
        if (_hammer != null && _hammerS.enabled == false)
        {
            Destroy( _hammer );
        }

        saveData.LoadPlayerAnimal();
        
    } //END InitValues()


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
            int _tempHealth = health;
            damageSound.Play();


            //Check if player is at 0 or less health
            try
            {
                //Try to determine death state
                CheckForDeath();
                Debug.Log("Player health is greater than zero, can be passed");
            }
            catch(System.ArgumentException e)
            {
                //If health is less than 0
                health = 0;
                CheckForDeath();
                Debug.Log("Player health reset to 0 " + e);
            }
            finally
            {
                //Health has been checked
                Debug.Log("Player health checked for death");
            }


            //Display current health, this way it never shows less than 0 health
            hpText.text = "Health = " + health.ToString();
        }
    } //END LoseHealth()

    /// <summary>
    /// Check if the player has dies
    /// </summary>
    public void CheckForDeath()
    {
       /*if(health < 0)
        {
            throw new System.Exception("Player health cannot be less than 0");
        }*/

        //Check if player is at 0 health
        if (health <= 0)
        {
            //Disable weapons
            //Debug.Log("Player dies");

            onPlayerDeath?.Invoke();


           // xp = 0;
            //Reset level and load menu
            level = 0;

            FindAnyObjectByType<ResultsScreen>().ShowResults();
            //SceneManager.LoadScene(0);
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
