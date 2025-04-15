using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //public GameObject[] enemies;
    public GameObject player;
    public List<GameObject> spawns;

    public GameObject rat1;
    public GameObject rat2;
    public GameObject clown;
    public GameObject bear;

    public float spawnDistanceMin = 10f;
    public float spawnDistanceMax = 14f;
    public float spawnWait;
    public float spawnMostWait = 12f;
    public float spawnLeastWait = 6f;
    public int startWait;
    public bool stop;
    public float timeToIncrease = 4f;
    public float timeToAddBear = 60f;
    public float timeToAddClown = 90f;
    public float timeToAddEnemy = 60f;
    public float increaseAmount = 0.5f;

    private float timer;
    private float addBearTimer;
    private float addClownTimer;
    private float addEnemyTimer;
    private float randomRangeSpawnTimer;

    public int randomRangeSpawn = 8;
    private int randomEnemy;

    //PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        StartCoroutine(waitSpawner());
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        timer += Time.deltaTime;
        addBearTimer += Time.deltaTime;
        addClownTimer += Time.deltaTime;
        randomRangeSpawnTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timer >= timeToIncrease)
        {
            if (spawnMostWait > 4f) // Prevent negative or very small intervals
            {
                spawnMostWait -= increaseAmount;
            }
            timer = 0f;
        }

        if (addBearTimer >= timeToAddBear)
        {
            spawns.Add(bear);
            addBearTimer = 0f;
        }

        if (addClownTimer >= timeToAddClown)
        {
            spawns.Add(clown);
            addClownTimer = 0f;
        }

        if (addEnemyTimer >= timeToAddEnemy)
        {

        }

        if (randomRangeSpawnTimer >= 60f)
        {
            randomRangeSpawn++;
            randomRangeSpawnTimer = 0f;
        }
    }

    IEnumerator waitSpawner()
    {
        Vector3 playerPosition = player.transform.position;
        
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            randomEnemy = Random.Range(0, randomRangeSpawn);
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            Instantiate(spawns[randomEnemy], spawnPosition, player.transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
        
}