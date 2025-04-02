using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class GameManger : MonoBehaviour
{
    public GameObject player;
    public Transform playerSpawnpoint;
    private GameObject playerInstance;

    public bool newGame;

    // Start is called before the first frame update
    void Awake()
    {

        //If there is no player, aka this is the first round the player has done, spawn in a player
        if(FindAnyObjectByType<PlayerMovement>() == null)
        {
            SpawnPlayer();
        }
        //If player has played already
        else
        {
            playerInstance = FindAnyObjectByType<PlayerMovement>().gameObject;
            playerInstance.transform.position = playerSpawnpoint.position;
        }
        
       
    }

    void SpawnPlayer()
    {
        // Spawns player at PlayerSpawnpoint
        playerInstance = Instantiate(player, playerSpawnpoint.position, playerSpawnpoint.rotation);
        SaveData _save = playerInstance.GetComponent<SaveData>();
        if (!newGame)
        {
           
            _save.LoadPlayerData();
            _save.LoadUpgradeData();
        }
        else if(newGame)
        {
            _save.ClearData();
        }
    }

}
