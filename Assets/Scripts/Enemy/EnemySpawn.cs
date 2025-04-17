using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //public GameObject[] enemies;
    public GameObject player;
    public List<GameObject> spawns1;
    public List<GameObject> spawns2;
    public List<GameObject> spawns3;

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
    public float timeForSpawn2List = 60f;
    public float timeForSpawn3List = 120f;
    public float increaseAmount = 0.5f;

    private bool spawns1Active;
    private bool spawns2Active;
    private bool spawns3Active;

    private float timerMostWait;
    private float timerNextList;
    private int randomEnemy;


    //Object pooling
    [SerializeField] private int poolSize;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(waitSpawner());
        spawns1Active = true;
        spawns2Active = false;
        spawns3Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        timerMostWait += Time.deltaTime;
        timerNextList += Time.deltaTime;

        if (timerNextList >= timeForSpawn2List)
        {
            spawns1Active = false;
            spawns2Active = true;
            //Debug.Log("Spawn2 active");
        }

        if (timerNextList >= timeForSpawn3List)
        {
            spawns2Active = false;
            spawns3Active = true;
            //Debug.Log("Spawn3 active");
        }
    }

    private void FixedUpdate()
    {
        if (timerMostWait >= timeToIncrease)
        {
            if (spawnMostWait > 4f) // Prevent negative or very small intervals
            {
                spawnMostWait -= increaseAmount;
            }

            timerMostWait = 0f;
        }
    }

    IEnumerator waitSpawner()
    {
        Vector3 playerPosition = player.transform.position;
        
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            if (spawns1.Count == 0 || spawns2.Count == 0 || spawns3.Count == 0)
            {
                //Debug.LogWarning("No enemies in spawn list!");
                yield return null;
                continue;
            }
            
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

            if (spawns1Active == true)
            {
                randomEnemy = Random.Range(0, spawns1.Count);
                Instantiate(spawns1[randomEnemy], spawnPosition, player.transform.rotation);
            }
            else if(spawns2Active == true)
            {
                randomEnemy = Random.Range(0, spawns2.Count);
                Instantiate(spawns2[randomEnemy], spawnPosition, player.transform.rotation);
            }
            else if(spawns3Active == true)
            {
                randomEnemy = Random.Range(0, spawns3.Count);
                Instantiate(spawns3[randomEnemy], spawnPosition, player.transform.rotation);
            }

            yield return new WaitForSeconds(spawnWait);
        }
    }

    #region Object Pooling

    void CreatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            int _idx = Random.Range(0, poolSize - i);

            if(_idx == 0)
            {
               
            }
        }
    }

    #endregion

}