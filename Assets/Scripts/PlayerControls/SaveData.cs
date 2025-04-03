using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SaveData : MonoBehaviour
{
    [SerializeField] private UpgradeScriptObj health;
    [SerializeField] private UpgradeScriptObj xp;
    [SerializeField] private UpgradeScriptObj mag;

    [Header("Only on player")]
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Only on main menu")]
    [SerializeField] private GameObject storeButton;
    [SerializeField] private TextMeshProUGUI levelText;
    public bool reset = true;

    //Keys:
    //AffectOnHealth.txt
    //AffectOnXP.txt
    //AffectOnSpeed.txt
    //AffectOnMag.txt
    //MagSpeed.txt
    //HealthLevel.txt
    //XPLevel.txt
    //MagLevel.txt
    //HighScore.txt
    //Money.txt
    //Lion.txt - bool but int not set 
    //Elephant.txt - bool but int not set

    //Add more as upgrades are made

    private void Start()
    {
        /*if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadUpgradeData();
            if(reset == false)
            {
                storeButton.SetActive(true);
                Debug.Log("Reset false");
            }
        }*/
        LoadHighScore();
    }

    #region Saving
    public void SaveMoney()
    {
        string _path = Application.persistentDataPath + "/Money.txt";
        File.WriteAllText(_path, playerHealth.money.ToString());
        Debug.Log(_path);
    }

    public void SaveHighScore(int _score) //Right now set ti highest level in a run
    {
        string _path = Application.persistentDataPath + "/HighScore.txt";
        File.WriteAllText(_path, _score.ToString());
        Debug.Log(_path);
       
    }

    public void SaveHealthUpgrade()
    {
        string _path = Application.persistentDataPath + "/AffectOnHealth.txt";
        File.WriteAllText(_path, health.affectOnHealth.ToString());
        Debug.Log(_path);

        string _path1 = Application.persistentDataPath + "/HealthLevel.txt";
        File.WriteAllText(_path1, health.level.ToString());
        Debug.Log(_path1);
    }

    public void SaveXPUpgrade()
    {
        string _path = Application.persistentDataPath + "/AffectOnXP.txt";
        File.WriteAllText(_path, xp.affectOnXP.ToString());
        Debug.Log(_path);

        string _path1 = Application.persistentDataPath + "/XPLevel.txt";
        File.WriteAllText(_path1, xp.level.ToString());
        Debug.Log(_path1);
    }

    public void SaveMagUpgrade()
    {
        string _path = Application.persistentDataPath + "/AffectOnMag.txt";
        File.WriteAllText(_path, mag.affectOnMag.ToString());
        Debug.Log(_path);

        string _path1 = Application.persistentDataPath + "/MagLevel.txt";
        File.WriteAllText(_path1, mag.level.ToString());
        Debug.Log(_path1);
    }
    #endregion

    #region Loading
    //Call on continue game
    public void LoadPlayerData()
    {
        string _path = Application.persistentDataPath + "/Money.txt";
        if(File.Exists(_path))
        {
            string _val = File.ReadAllText(_path);
            playerHealth.money = int.Parse(_val);
            Debug.Log("Money loaded: " + playerHealth.money);
        }

    }

    public void LoadHighScore()
    {
        string _path = Application.persistentDataPath + "/HighScore.txt";
        if(File.Exists(_path))
        {
            string _val = File.ReadAllText(_path);
            int _level = int.Parse(_val);
            levelText.text = "Highest Level Achieved: " + _level;
            Debug.Log("Highest level loaded: " + _level);
        }
        else
        {
            levelText.text = "Highest Level Achieved: " + 0.ToString();
        }
    }

    //Call on continue game
    public void LoadUpgradeData()
    {
        //Get affect on health for upgrade
        string _pathHealth = Application.persistentDataPath + "/AffectOnHealth.txt";
        if (File.Exists(_pathHealth))
        {
            string _val = File.ReadAllText(_pathHealth);
            health.affectOnHealth = int.Parse(_val);
            Debug.Log("Affect on health loaded: " + health.affectOnHealth);
            reset = false;
        }

        //Get health level
        string _pathHealthLvl = Application.persistentDataPath + "/HealthLevel.txt";
        if (File.Exists(_pathHealthLvl))
        {
            string _val = File.ReadAllText(_pathHealthLvl);
            health.level = int.Parse(_val);
            Debug.Log("Health upgrade level loaded: " + health.level);
            reset = false;
        }

        //Get affect on xp for upgrade
        string _pathXP = Application.persistentDataPath + "/AffectOnXP.txt";
        if (File.Exists(_pathXP))
        {
            string _val = File.ReadAllText(_pathXP);
            xp.affectOnXP = int.Parse(_val);
            Debug.Log("Affect on xp loaded: " + xp.affectOnXP);
            reset = false;
        }

        //Get xp level
        string _pathXPLvl = Application.persistentDataPath + "/XPLevel.txt";
        if (File.Exists(_pathXPLvl))
        {
            string _val = File.ReadAllText(_pathXPLvl);
            xp.level = int.Parse(_val);
            Debug.Log("XP level loaded" + xp.level);
            reset = false;
        }

        //Get affect on magnetism for upgrade
        string _pathMag = Application.persistentDataPath + "/AffectOnMag.txt";
        if (File.Exists(_pathMag))
        {
            string _val = File.ReadAllText(_pathMag);
            mag.affectOnMag = int.Parse(_val);
            Debug.Log("Affect on magnetism loaded" + mag.affectOnMag);
            reset = false;
        }

        //Get magnetism level
        string _pathMagLvl = Application.persistentDataPath + "/MagLevel.txt";
        if (File.Exists(_pathMagLvl))
        {
            string _val = File.ReadAllText(_pathMagLvl);
            mag.level = int.Parse(_val);
            Debug.Log("Magnetism level loaded:" + mag.level);
            reset = false;
        }

        
    }

    //Call on new game
    public void ClearData()
    {
        //Player variables
        //Player money
        string _path = Application.persistentDataPath + "/Money.txt";
        if (File.Exists(_path))
        {
            File.WriteAllText(_path, 0.ToString());
        }

        //Player high score
        string _pathScore = Application.persistentDataPath + "/HighScore.txt";
        if (File.Exists(_pathScore))
        {
            File.WriteAllText(_pathScore, 0.ToString());
        }

        //If lion unlocked
        string _pathLion = Application.persistentDataPath + "Lion.txt";
        if (File.Exists(_pathLion))
        {
            File.WriteAllText(_pathLion, 0.ToString());
        }

        //If elephant unlocked
        string _pathElephant = Application.persistentDataPath + "Elephant.txt";
        if (File.Exists(_pathElephant))
        {
            File.WriteAllText(_pathElephant, 0.ToString());
        }

        //Upgrade variables
        //Get affect on health for upgrade
        string _pathHealth = Application.persistentDataPath + "/AffectOnHealth.txt";
        if (File.Exists(_pathHealth))
        {
            File.WriteAllText (_pathHealth, 10.ToString());
        }

        //Get health level
        string _pathHealthLvl = Application.persistentDataPath + "/HealthLevel.txt";
        if (File.Exists(_pathHealthLvl))
        {
            File.WriteAllText(_pathHealthLvl, 0.ToString());
        }

        //Get affect on xp for upgrade
        string _pathXP = Application.persistentDataPath + "/AffectOnXP.txt";
        if (File.Exists(_pathXP))
        {
            File.WriteAllText(_pathXP, 2.ToString());
        }

        //Get xp level
        string _pathXPLvl = Application.persistentDataPath + "/XPLevel.txt";
        if (File.Exists(_pathXPLvl))
        {
            File.WriteAllText(_pathXPLvl, 0.ToString());
        }

        //Get affect on magnetism for upgrade
        string _pathMag = Application.persistentDataPath + "/AffectOnMag.txt";
        if (File.Exists(_pathMag))
        {
            File.WriteAllText( _pathMag, 5.ToString());
        }

        //Get magnetism level
        string _pathMagLvl = Application.persistentDataPath + "/MagLevel.txt";
        if (File.Exists(_pathMagLvl))
        {
            File.WriteAllText(_pathMagLvl, 0.ToString());
        }


        LoadPlayerData();
        LoadUpgradeData();
        levelText.text = "Highest Level Achieved: " + 0.ToString();
        reset = true;
    }
    #endregion
}
