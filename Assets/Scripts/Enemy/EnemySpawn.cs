using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;

    public float spawnDistanceMin = 8f;
    public float spawnDistanceMax = 12f;
    public float spawnWait;
    public float spawnMostWait = 5f;
    public float spawnLeastWait = 0.5f;
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
            randomEnemy = Random.Range(0, 3);
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            Instantiate(enemies[randomEnemy], spawnPosition, player.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
