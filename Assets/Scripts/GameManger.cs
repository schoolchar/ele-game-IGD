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

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnPlayer()
    {
        // Spawns player at PlayerSpawnpoint
        playerInstance = Instantiate(player, playerSpawnpoint.position, playerSpawnpoint.rotation);
    }

}
