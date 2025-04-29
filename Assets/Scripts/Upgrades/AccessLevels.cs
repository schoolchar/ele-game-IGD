using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccessLevels : MonoBehaviour
{
    //Made for Char - for UI in game scene for upgrade activation

    [SerializeField] UpgradeScriptObj healthUpgrade;
    [SerializeField] UpgradeScriptObj xpUpgrade;
    [SerializeField] UpgradeScriptObj speedUpgrade;
    [SerializeField] UpgradeScriptObj lifeForceUpgrade;
    [SerializeField] UpgradeScriptObj forcefieldUpgrade;

    //All things that are on the player that need to be loaded every time they start the game
    Ringoffire ringOfFire;
    Knifethrow knifeThrow;
    [SerializeField] GameObject oldPlayer;
    PlayerMovement[] allPlayers;
    EnemySpawn enemySpawn;

    //To access the level of any of these upgrades:
    //Do the variable associated w the upgrade and do .level after it
    //eg the health upgrade's level is accessed by healthUpgrade.level

    // UI Text references for displaying upgrade levels
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private TextMeshProUGUI xpText;

    [SerializeField] private TextMeshProUGUI speedText;

    [SerializeField] private TextMeshProUGUI lifeForceText;

    [SerializeField] private TextMeshProUGUI forcefieldText;

    private void Start()
    {
        // Updates pause menu with upgrade levels, uncomment when put in scene
        UpdateUI();

        //Loading
        ringOfFire = FindAnyObjectByType<Ringoffire>(FindObjectsInactive.Include);
        ringOfFire.InitOnLoad();

        knifeThrow = FindAnyObjectByType<Knifethrow>();
        knifeThrow.InitOnLoad();

        enemySpawn = FindAnyObjectByType<EnemySpawn>();
        enemySpawn.InitOnLoad();

        //Deal with loading in multiple players
       /* allPlayers = FindObjectsOfType<PlayerMovement>();
        if(allPlayers.Length > 1)
        {
            GameObject _newPlayer = null;
            for(int i = 0; i < allPlayers.Length; i++)
            {
                if (allPlayers[i].GetComponent<OldPlayerID>() == null)
                {
                    _newPlayer = allPlayers[i].gameObject;
                }
            }

            _newPlayer.transform.position = oldPlayer.transform.position;
            Destroy(oldPlayer);
        }*/

    }

    //takes upgrades levels and sets to a string then to text
    private void UpdateUI()
    {
        healthText.text = "Health Upgrade: " + healthUpgrade.level.ToString();
        xpText.text = "XP Upgrade: " + xpUpgrade.level.ToString();
        speedText.text = "Speed Upgrade: " + speedUpgrade.level.ToString();
        lifeForceText.text = "Life Force Upgrade: " + lifeForceUpgrade.level.ToString();
        forcefieldText.text = "Forcefield Upgrade: " + forcefieldUpgrade.level.ToString();
    }
}
