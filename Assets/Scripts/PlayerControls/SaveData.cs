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
    public UpgradeScriptObj health;
    public UpgradeScriptObj xp;
    public UpgradeScriptObj mag;
    public UpgradeScriptObj speed;
    public UpgradeScriptObj lifeForce;
    public UpgradeScriptObj forcefield;

    private string[] defeaultAnim = new string[5] {"1", "0", "0", "0", "0"};

    [Header("Only on player")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ForcefieldOnPlayer forcefieldOnPlayer;
    //Animals
    public Elephant elephantComp;
    public Lion lionComp;
    public SeaLion seaLionComp;
    public Monkey monkeyComp;
    public BasicShoot baseComp;

    public CharacterID[] playerModels;
    public GameObject characterActive;

    [Header("Only on main menu")]
    [SerializeField] private GameObject storeButton;
    [SerializeField] private TextMeshProUGUI levelText;
    public bool reset = true;

    //Keys:
    //AffectOnHealth.txt
    //AffectOnXP.txt
    //AffectOnSpeed.txt
    //AffectOnMag.txt
    //AffectOnSpeed.txt
    //MagSpeed.txt
    //AffectOnLifeForce.txt
    //ForceFieldActive.txt
    //HealthLevel.txt
    //XPLevel.txt
    //MagLevel.txt
    //SpeedLevel.txt
    //LifeForceLevel.txt
    //ForcefieldLevel.txt
    //HighScore.txt
    //Money.txt
    //Lion.txt - bool but int not set 
    //Elephant.txt - bool but int not set
    //Animals.txt -> 0 Lion, 1 Elephant, 2 Monkey, 3 Seal, 5 last active animal

    //Add more as upgrades are made

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            /*LoadUpgradeData();
            if(reset == false)
            {
                storeButton.SetActive(true);
                Debug.Log("Reset false");
            }*/
            LoadHighScore();
        }

        if(SceneManager.GetActiveScene().buildIndex == 1)
            PlayerHealth.onPlayerDeath += CALLBACK_CheckForSaveHighScore;

        string _pathA = Application.persistentDataPath + "/Animals.txt";
        if(File.Exists(_pathA) == false)
        {
            Debug.Log("Path does not exist");
            File.WriteAllLines(_pathA, defeaultAnim);
        } 
        else
        {
            //Debug.Log(_pathA);
        }

        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            LoadPlayerData();
        }
        LoadPlayerAnimal();
    }

    #region Saving
    //Save the amount of money the player has
    public void SaveMoney()
    {
        string _path = Application.persistentDataPath + "/Money.txt";
        File.WriteAllText(_path, playerHealth.money.ToString());
        //Debug.Log(_path);
    }

    //Save the highest level the player has reached in one run
    public void SaveHighScore(int _score) 
    {
        string _path = Application.persistentDataPath + "/HighScore.txt";
        File.WriteAllText(_path, _score.ToString());
        //Debug.Log(_path);
       
    }

    //Store data for affect on max health and max health upgrade lavel
    public void SaveHealthUpgrade()
    {
        //Affect on max health
        string _path = Application.persistentDataPath + "/AffectOnHealth.txt";
        File.WriteAllText(_path, health.affectOnHealth.ToString());
        //Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/HealthLevel.txt";
        File.WriteAllText(_path1, health.level.ToString());
        //Debug.Log(_path1);

        //Cost
        string _path2 = Application.persistentDataPath + "/HealthCost.txt";
        File.WriteAllText(_path2, health.cost.ToString());
    }


    //Store data for affect on XP and XP upgrade level
    public void SaveXPUpgrade()
    {
        //Affect on xp
        string _path = Application.persistentDataPath + "/AffectOnXP.txt";
        File.WriteAllText(_path, xp.affectOnXP.ToString());
        //Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/XPLevel.txt";
        File.WriteAllText(_path1, xp.level.ToString());
        //Debug.Log(_path1);

        //Cost
        string _path2 = Application.persistentDataPath + "/XPCost.txt";
        File.WriteAllText(_path2, xp.cost.ToString());
    }

    //Store data for affect on magnetism and level of magnetism upgrade
    public void SaveMagUpgrade()
    {
        //Affect on mag
        string _path = Application.persistentDataPath + "/AffectOnMag.txt";
        File.WriteAllText(_path, mag.affectOnMag.ToString());
       // Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/MagLevel.txt";
        File.WriteAllText(_path1, mag.level.ToString());
        //Debug.Log(_path1);

    }


    //Store data for affect on speed and speed upgrade level
    public void SaveSpeedUpgrade()
    {
        //Affect on speed
        string _path = Application.persistentDataPath + "/AffectOnSpeed.txt";
        File.WriteAllText(_path, speed.affectOnSpeed.ToString());
        //Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/SpeedLevel.txt";
        File.WriteAllText(_path1, speed.level.ToString());
        //Debug.Log(_path1);

        //Cost
        string _path2 = Application.persistentDataPath + "/SpeedCost.txt";
        File.WriteAllText(_path2, speed.cost.ToString());
    }

    //Store data for life force upgrade and level
    public void SaveLifeForceUpgrade()
    {
        //Affect on health for life force, amount of health gained by killing an enemy
        string _path = Application.persistentDataPath + "/AffectOnLifeForce.txt";
        File.WriteAllText(_path, lifeForce.affectOnHealth.ToString());
        //Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/LifeForceLevel.txt";
        File.WriteAllText(_path1, lifeForce.level.ToString());
        //Debug.Log(_path1);

        //Cost
        string _path2 = Application.persistentDataPath + "/LifeForceCost.txt";
        File.WriteAllText(_path2, lifeForce.cost.ToString());
    }

    public void SaveForcefieldUpgrade()
    {
        string _path = Application.persistentDataPath + "/ForcefieldActive.txt";
        File.WriteAllText(_path, forcefield.deactivateTimeFF.ToString()); //For now, this saves either 1 or nothing, 1 if activated, later make it save the time between activation
        //Debug.Log(_path);

        //Level
        string _path1 = Application.persistentDataPath + "/ForcefieldLevel.txt";
        File.WriteAllText(_path1, forcefield.level.ToString());
        //Debug.Log(_path1);

        //Cost
        string _path2 = Application.persistentDataPath + "/ForcefieldCost.txt";
        File.WriteAllText(_path2, forcefield.cost.ToString());
    }

    //Animals
    public void SaveUnlockedAnimals(int _index)
    {
        string _path = Application.persistentDataPath + "/Animals.txt";
        string[] tmp = File.ReadAllLines(_path);
        tmp[_index] = "1";
        tmp[4] = _index.ToString();
        File.WriteAllLines(_path, tmp); 
    }
    #endregion

    #region Loading
    //Call on continue game
    public void LoadPlayerData()
    {
        //get path to money data, if it exists, show money in store
        string _path = Application.persistentDataPath + "/Money.txt";
        if(File.Exists(_path))
        {
            string _val = File.ReadAllText(_path);
            if (playerHealth != null)
            {
                playerHealth.money = int.Parse(_val);
                Debug.Log("Money loaded: " + playerHealth.money);
            }
        }

    }

    public void LoadHighScore()
    {
        //get path to high score, if exists, then read and apply to UI
        string _path = Application.persistentDataPath + "/HighScore.txt";
        if(File.Exists(_path))
        {
            string _val = File.ReadAllText(_path);
            int _level = int.Parse(_val);
            levelText.text = "Highest Level Achieved: " + _level;
            //Debug.Log("Highest level loaded: " + _level);
        }
        else
        {
            //If file does not exist yet, set text to show 0
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
           // Debug.Log("Affect on health loaded: " + health.affectOnHealth);
            reset = false;
        }

        //Get health level
        string _pathHealthLvl = Application.persistentDataPath + "/HealthLevel.txt";
        if (File.Exists(_pathHealthLvl))
        {
            string _val = File.ReadAllText(_pathHealthLvl);
            health.level = int.Parse(_val);
           // Debug.Log("Health upgrade level loaded: " + health.level);
            reset = false;
        }

        //Get cost of health
        string _pathHealthCost = Application.persistentDataPath + "/HealthCost.txt";
        if (File.Exists(_pathHealthCost))
        {
            string _val = File.ReadAllText(_pathHealthCost);
            health.cost = int.Parse(_val);
            reset = false;
        }

        //Get affect on xp for upgrade
        string _pathXP = Application.persistentDataPath + "/AffectOnXP.txt";
        if (File.Exists(_pathXP))
        {
            string _val = File.ReadAllText(_pathXP);
            xp.affectOnXP = int.Parse(_val);
            //Debug.Log("Affect on xp loaded: " + xp.affectOnXP);
            reset = false;
        }

        //Get xp level
        string _pathXPLvl = Application.persistentDataPath + "/XPLevel.txt";
        if (File.Exists(_pathXPLvl))
        {
            string _val = File.ReadAllText(_pathXPLvl);
            xp.level = int.Parse(_val);
            //Debug.Log("XP level loaded" + xp.level);
            reset = false;
        }

        string _pathXPCost = Application.persistentDataPath + "/XPCost.txt";
        if(File.Exists(_pathXPCost))
        {
            string _val = File.ReadAllText(_pathXPCost);
            xp.cost = int.Parse(_val);
            reset = false;
        }

        //Get affect on magnetism for upgrade
        string _pathMag = Application.persistentDataPath + "/AffectOnMag.txt";
        if (File.Exists(_pathMag))
        {
            string _val = File.ReadAllText(_pathMag);
            mag.affectOnMag = int.Parse(_val);
           // Debug.Log("Affect on magnetism loaded" + mag.affectOnMag);
            reset = false;
        }

        //Get magnetism level
        string _pathMagLvl = Application.persistentDataPath + "/MagLevel.txt";
        if (File.Exists(_pathMagLvl))
        {
            string _val = File.ReadAllText(_pathMagLvl);
            mag.level = int.Parse(_val);
            //Debug.Log("Magnetism level loaded:" + mag.level);
            reset = false;
        }

        //Get affect on speed for upgrade
        string _pathSpeed = Application.persistentDataPath + "/AffectOnSpeed.txt";
        if (File.Exists(_pathSpeed))
        {
            string _val = File.ReadAllText(_pathSpeed);
            speed.affectOnSpeed = int.Parse(_val);
            //Debug.Log("Affect on magnetism loaded" + speed.affectOnSpeed);
            reset = false;
        }

        //Get Speed level
        string _pathSpeedLvl = Application.persistentDataPath + "/SpeedLevel.txt";
        if (File.Exists(_pathSpeedLvl))
        {
            string _val = File.ReadAllText(_pathSpeedLvl);
            speed.level = int.Parse(_val);
            //Debug.Log("Magnetism level loaded:" + speed.level);
            reset = false;
        }

        string _pathSpeedCost = Application.persistentDataPath + "/SpeedCost.txt";
        if(File.Exists(_pathSpeedCost))
        {
            string _val = File.ReadAllText(_pathSpeedCost);
            speed.cost = int.Parse(_val);
            reset = false;
        }

        //Get life force affect on health for upgrade
        string _pathLifeForce = Application.persistentDataPath + "/AffectOnLifeForce.txt";
        if (File.Exists(_pathLifeForce))
        {
            string _val = File.ReadAllText(_pathLifeForce);
            lifeForce.affectOnHealth = int.Parse(_val);
            //Set player health health per enemy
            if(playerHealth != null)
            {
                playerHealth.healthPerEnemy += int.Parse(_val);
                //Debug.Log("Affect on life force loaded" + lifeForce.affectOnHealth);
            }
            
            reset = false;
        }

        //Get life force level
        string _pathLifeForceLvl = Application.persistentDataPath + "/LifeForceLevel.txt";
        if (File.Exists(_pathLifeForceLvl))
        {
            string _val = File.ReadAllText(_pathLifeForceLvl);
            lifeForce.level = int.Parse(_val);
            //Set life force to true on player if the level is greater than 0
            if(lifeForce.level > 0)
            {
                playerHealth.lifeForce = true;
            }
           // Debug.Log("Life force level loaded:" + lifeForce.level);
            reset = false;
        }

        string _pathLifeForceCost = Application.persistentDataPath + "/LifeForceCost.txt";
        if(File.Exists(_pathLifeForceCost))
        {
            string _val = File.ReadAllText (_pathLifeForceCost);
            lifeForce.cost = int.Parse(_val);
            reset = false;
        }

        //Set forcefield upgrade active or inactive
        string _pathForcefield = Application.persistentDataPath + "/ForcefieldActive.txt";
        if (File.Exists(_pathForcefield))
        {
            //Read value
            string _val = File.ReadAllText (_pathForcefield);
            forcefield.deactivateTimeFF = int.Parse(_val);
            //Checks if forcefield is on or off
            if(forcefield.deactivateTimeFF > 3 && forcefieldOnPlayer != null)
            {
                forcefieldOnPlayer.forcefieldActive = true;
                forcefieldOnPlayer.deactivateTime = forcefield.deactivateTimeFF;
                //StartCoroutine(forcefieldOnPlayer.TimeForcefield());
                
            }
            reset = false;
        }

        string _pathForceFieldLvl = Application.persistentDataPath + "/ForcefieldLevel.txt";
        if (File.Exists(_pathForceFieldLvl))
        {
            string _val = File.ReadAllText(_pathForceFieldLvl);
            forcefield.level = int.Parse(_val);
            //Debug.Log("Forcefield level loaded:" + forcefield.level);
            reset = false;
        }

        string _pathForcefieldCost = Application.persistentDataPath + "/ForcefieldCost.txt";
        if (File.Exists(_pathForcefieldCost))
        {
            string _val = File.ReadAllText(_pathForcefieldCost);
            forcefield.cost = int.Parse(_val);
            reset = false;
        }

    }



    public void LoadPlayerAnimal()
    {
        string _path = Application.persistentDataPath + "/Animals.txt";
        if(File.Exists(_path) && GetComponent<PlayerHealth>() != null)
        {
            string[] tmp = File.ReadAllLines (_path);
            int _activeAnimal = int.Parse(tmp[4]);

            if (_activeAnimal == 0) //Lion
            {
                Debug.Log("Load lion character");
                //Enable lion, disable everything else
                lionComp.enabled = true;
                elephantComp.enabled = false;
                baseComp.enabled = false;
                monkeyComp.enabled = false;
                seaLionComp.enabled = false;

                characterActive.SetActive(false);
                for (int i = 0; i < playerModels.Length; i++)
                {
                    if (playerModels[i].animal == "Lion")
                    {
                        playerModels[i].gameObject.SetActive(true);
                        characterActive = playerModels[i].gameObject;
                    }
                }
            }
            else if (_activeAnimal == 1) //Elephant
            {
                Debug.Log("Load elephant character");
                //Enable elephant. disable everything else
                lionComp.enabled = false;
                elephantComp.enabled = true;
                seaLionComp.enabled = false;
                monkeyComp.enabled = false;
                baseComp.enabled = false;

                characterActive.SetActive(false);
                for (int i = 0; i < playerModels.Length; i++)
                {
                    if (playerModels[i].animal == "Elephant")
                    {
                        playerModels[i].gameObject.SetActive(true);
                        characterActive = playerModels[i].gameObject;
                    }
                }
            }
            else if (_activeAnimal == 2) //Monkey
            {
                Debug.Log("Load monkey character");
                monkeyComp.enabled = true;
                seaLionComp.enabled = false;
                lionComp.enabled = false;
                baseComp.enabled = false;
                elephantComp.enabled = false;

                characterActive.SetActive(false);
                for (int i = 0; i < playerModels.Length; i++)
                {
                    if (playerModels[i].animal == "Monkey")
                    {
                        playerModels[i].gameObject.SetActive(true);
                        characterActive = playerModels[i].gameObject;
                    }
                }
            }
            else if (_activeAnimal == 3) //Seal
            {
                Debug.Log("Load seal character");
                seaLionComp.enabled = true;
                lionComp.enabled = false;
                monkeyComp.enabled = false;
                baseComp.enabled = false;
                elephantComp.enabled = false;

                characterActive.SetActive(false);
                for (int i = 0; i < playerModels.Length; i++)
                {
                    if (playerModels[i].animal == "Seal")
                    {
                        playerModels[i].gameObject.SetActive(true);
                        characterActive = playerModels[i].gameObject;
                    }
                }

            }
        }
        else
        {
            Debug.Log("Failed to load animal");
        }
        

    }
    #endregion

    #region Clear Data
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

        //Get affect on speed for upgrade
        string _pathSpeed = Application.persistentDataPath + "/AffectOnSpeed.txt";
        if(File.Exists(_pathSpeed))
        {
            File.WriteAllText(_pathSpeed, 50.ToString());
        }

        //Get Speed level
        string _pathSpeedLvl = Application.persistentDataPath + "/SpeedLevel.txt";
        if(File.Exists(_pathSpeedLvl))
        {
            File.WriteAllText(_pathSpeedLvl, 0.ToString());
        }


        //Get affect on health for life force upgrade
        string _pathLifeForce = Application.persistentDataPath + "/AffectOnLifeForce.txt";
        if (File.Exists(_pathLifeForce))
        {
            File.WriteAllText(_pathLifeForce, 1.ToString());
        }

        //Get life force level
        string _pathLifeForceLvl = Application.persistentDataPath + "/LifeForceLevel.txt";
        if (File.Exists(_pathLifeForceLvl))
        {
            File.WriteAllText(_pathLifeForceLvl, 0.ToString());
        }

        //Set life force in player health to false
        if(playerHealth != null)
        {
            playerHealth.lifeForce = false;
        }

        //Get affect on health for life force upgrade
        string _pathForcefield = Application.persistentDataPath + "/ForcefieldActive.txt";
        if (File.Exists(_pathForcefield))
        {
            File.WriteAllText(_pathForcefield, 3.ToString());
        }

        //Get life force level
        string _pathForcefieldLvl = Application.persistentDataPath + "/ForcefieldLevel.txt";
        if (File.Exists(_pathForcefieldLvl))
        {
            File.WriteAllText(_pathForcefieldLvl, 0.ToString());
        }

        string _pathHealthCost = Application.persistentDataPath + "/HealthCost.txt";
        if (File.Exists(_pathHealthCost))
        {
            File.WriteAllText(_pathHealthCost, 30.ToString());
        }

        string _pathXPCost = Application.persistentDataPath + "/XPCost.txt";
        if (File.Exists(_pathXPCost))
        {
            File.WriteAllText(_pathXPCost, 25.ToString());
        }

        string _pathSpeedCost = Application.persistentDataPath + "/SpeedCost.txt";
        if (File.Exists(_pathSpeedCost))
        {
            File.WriteAllText(_pathSpeedCost, 60.ToString());
        }

        string _pathLifeForceCost = Application.persistentDataPath + "/LifeForceCost.txt";
        if(File.Exists(_pathLifeForceCost))
        {
            File.WriteAllText(_pathLifeForceCost, 120.ToString());
        }

        string _pathForcefieldCost = Application.persistentDataPath + "/ForcefieldCost.txt";
        if(File.Exists(_pathForcefieldCost))
        {
            File.WriteAllText(_pathForcefieldCost, 20.ToString());
        }

        string _pathAnimals = Application.persistentDataPath + "/Animals.txt";
        if(File.Exists(_pathAnimals))
        {
            File.WriteAllLines(_pathAnimals, defeaultAnim);
        }


        //Dialogue loading 
        PlayerPrefs.SetInt("MainMenuText", 0);
        PlayerPrefs.SetInt("StoreText", 0);
        PlayerPrefs.SetInt("GameText", 0);
        PlayerPrefs.Save();


        //Load in the reset values
        LoadPlayerData();
        LoadUpgradeData();

        //Set text and bool indicating if reset is true
        levelText.text = "Highest Level Achieved: " + 0.ToString();
        reset = true;
    }
    #endregion

    #region Events and Callbacks
    /// <summary>
    /// Callback for onPlayerDeath in player health, checks if the player has a new highest level, saves
    /// </summary>
    public void CALLBACK_CheckForSaveHighScore()
    {
       // Debug.Log("This is  = " + this.gameObject.name);
        //Check if this is the highest level the player has reached
        string _path = Application.persistentDataPath + "/HighScore.txt";
        if (File.Exists(_path))
        {
            //get path, if it is the highest, override data in file
            int _highScore = int.Parse(File.ReadAllText(_path));
            if (playerHealth.level > _highScore)
            {
                SaveHighScore(playerHealth.level);
            }
        }
        else
        {
            SaveHighScore(playerHealth.level);
        }
    } //END CALLBACK_CheckForSaveHighScore()


    #endregion
}
