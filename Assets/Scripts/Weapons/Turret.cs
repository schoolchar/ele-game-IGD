using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // Prefab turret
    public GameObject TurretPrefab;

    // Spawn timer
    public float spawnInterval = 20f;

    //dont spawn in player
    public float spawnDistance = 2f;

    //spawn above ground
    public float heightOffset = 1f;

    //player
    public Transform playerTransform;


    private void Start()
    {
        StartCoroutine(SpawnTurret());
    }

    private IEnumerator SpawnTurret()
    {
        while (true)
        {
            //makes the position not in player or ground
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance + Vector3.up * heightOffset;

            //spawns turret
            Instantiate(TurretPrefab, spawnPosition, Quaternion.identity);

            // Timer
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}