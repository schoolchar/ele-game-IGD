using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private UpgradeScriptObj health;
    [SerializeField] private UpgradeScriptObj xp;
    [SerializeField] private UpgradeScriptObj mag;

    [SerializeField] private PlayerHealth playerHealth;

    //Keys:
    //AffectOnHealth
    //AffectOnXP
    //AffectOnSpeed
    //AffectOnMag
    //MagSpeed
    //HealthLevel
    //XPLevel
    //MagLevel
    //HighScore
    //Money
    //Lion - bool but int not set 
    //Elephant - bool but int not set

    //Add more as upgrades are made
    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", playerHealth.money);
        PlayerPrefs.Save();
    }

    public void SaveHighScore() //Right now set ti highest level in a run
    {
         if(PlayerPrefs.GetInt("HighScore") < playerHealth.level || PlayerPrefs.HasKey("HighScore") == false)
         {
             PlayerPrefs.SetInt("HighScore", playerHealth.level);
            PlayerPrefs.Save();
         }
        
    }

    public void SaveHealthUpgrade()
    {
        PlayerPrefs.SetInt("AffectOnHealth", health.affectOnHealth);
        PlayerPrefs.SetInt("HealthLevel", health.level);
        PlayerPrefs.Save();
    }

    public void SaveXPUpgrade()
    {
        PlayerPrefs.SetInt("AffectOnXP", xp.affectOnXP);
        PlayerPrefs.SetInt("XPLevel", xp.level);
        PlayerPrefs.Save();
    }

    public void SaveMagUpgrade()
    {
        PlayerPrefs.SetInt("AffectOnMag", mag.affectOnMag);
        PlayerPrefs.SetInt("MagLevel", mag.level);
        PlayerPrefs.Save();
    }

    #region Loading
    //Call on continue game
    public void LoadPlayerData()
    {
        if(PlayerPrefs.HasKey("Money"))
        playerHealth.money = PlayerPrefs.GetInt("Money");
    }

    //Call on continue game
    public void LoadUpgradeData()
    {
        if(PlayerPrefs.HasKey("AffectOnHealth"))
        {
            health.affectOnHealth = PlayerPrefs.GetInt("AffectOnHealth");
            health.level = PlayerPrefs.GetInt("HealthLevel");
        }

        if(PlayerPrefs.HasKey("AffectOnXP"))
        {
            xp.affectOnXP = PlayerPrefs.GetInt("AffectOnXP");
            xp.level = PlayerPrefs.GetInt("XPLevel");
        }

        if(PlayerPrefs.HasKey("AffectOnMag"))
        {
            mag.affectOnMag = PlayerPrefs.GetInt("AffectOnMag");
            mag.level = PlayerPrefs.GetInt("MagLevel");
        }
    }

    //Call on new game
    public void ClearData()
    {
        PlayerPrefs.SetInt("Money", 0);
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("AffectOnHealth", 10);
        PlayerPrefs.SetInt("AffectOnXP", 2);
        PlayerPrefs.SetInt("AffectOnMag", 5);
        PlayerPrefs.SetInt("HealthLevel", 0);
        PlayerPrefs.SetInt("XPLevel", 0);
        PlayerPrefs.SetInt("MagLevel", 0);
        PlayerPrefs.SetInt("Lion", 0);
        PlayerPrefs.SetInt("Elephant", 0);
        PlayerPrefs.Save();
        LoadPlayerData();
        LoadUpgradeData();
    }
    #endregion
}
