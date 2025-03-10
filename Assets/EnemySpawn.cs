using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;

    public float minSpawnDistance = 5f;
    public float maxSpawnDistance = 10f;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;

    private int randomEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }

    IEnumerator waitSpawner()
    {
        Vector3 playerPosition = player.transform.position;

        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randomEnemy = Random.Range(0, 1);
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * distance;
            Instantiate(enemies[randomEnemy], spawnPosition, gameObject.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
