using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;

    public float spawnDistanceMin = 10f;
    public float spawnDistanceMax = 14f;
    public float spawnWait;
    public float spawnMostWait = 5f;
    public float spawnLeastWait = 2f;
    public int startWait;
    public bool stop;
    public float timeToIncrease = 25f;
    public float increaseAmount = 0.5f;

    private float timer;

    private int randomEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
            timer += Time.deltaTime;

            if (timer >= timeToIncrease)
            {
                if (spawnMostWait > 2.1f) // Prevent negative or very small intervals
                {
                    spawnMostWait -= increaseAmount;
                }
                timer = 0f;
            }

            randomEnemy = Random.Range(0, 4);
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            Instantiate(enemies[randomEnemy], spawnPosition, player.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
