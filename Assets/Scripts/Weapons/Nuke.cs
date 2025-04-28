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
            //spawns nuke
            GameObject spawnedNuke = Instantiate(nukePrefab, playerTransform.position, Quaternion.identity);

            //waits
            yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnNukes());
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
        }
    }
}