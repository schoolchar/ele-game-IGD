using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : WeaponBase
{
    //prefab
    public GameObject nukePrefab;

    //spawntime 
    public float spawnInterval = 30f;

    // Spawn position
    public Transform playerTransform;

    public override void ActivateThisWeapon()
    {
        // Start
        StartCoroutine(SpawnNukes());
    }

    private IEnumerator SpawnNukes()
    {
        while (true)
        {
            //spawns nuke
            GameObject spawnedNuke = Instantiate(nukePrefab, playerTransform.position, Quaternion.identity);

            //waits
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}