using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nuke : WeaponBase
{
    //prefab
    public GameObject nukePrefab;
    float scaleVal;
    //spawntime 
    public float spawnInterval = 30f;

    // Spawn position
    public Transform playerTransform;

    //Check for active
    public bool active;

    public override void ActivateThisWeapon()
    {
        if(active)
        {
            //spawnInterval -= 3f;
            scaleVal += scaleVal * 0.3f;
        }
        else
        {
            active = true;

            // Start
            StartCoroutine(SpawnNukes());
        }
        
    }

    private IEnumerator SpawnNukes()
    {
            //spawns nuke
            GameObject spawnedNuke = Instantiate(nukePrefab, playerTransform.position, Quaternion.identity);
        spawnedNuke.GetComponent<NukePrefab>().targetScale += scaleVal;
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