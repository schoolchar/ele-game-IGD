using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawn : MonoBehaviour
{
    [Header ("Lists")]
    public GameObject player;
    public List<GameObject> spawns1;
    public List<GameObject> spawns2;
    public List<GameObject> spawns3;

    [Header("Spawns")]    
    private int randomEnemy;

    [Header("Time")]
    public float spawnDistanceMin = 10f;
    public float spawnDistanceMax = 14f;
    public float spawnWait;
    private float spawnMostWait = 9f;
    private float spawnLeastWait = 4f;
    public bool stop;
    public float timeToIncrease = 4f;
    public float timeForSpawn2List = 60f;
    public float timeForSpawn3List = 120f;
    public float increaseAmount = 0.5f;
    private float timerMostWait;
    private float timerNextList;

    [Header("Bools")]
    public bool spawns1Active;
    public bool spawns2Active;
    public bool spawns3Active;
    private bool isInGameScene;

    //Object pooling
    [SerializeField] private int poolSize;

    // Start is called before the first frame update
    void Start()
    {
       InitOnLoad();
    }

    // Update is called once per frame
    void Update()
    {
        //Tells the spawner how long to wait before the next spawn
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        timerMostWait += Time.deltaTime;
        timerNextList += Time.deltaTime;

        if (timerNextList >= timeForSpawn2List)
        {
            spawns1Active = false;
            spawns2Active = true;
        }

        if (timerNextList >= timeForSpawn3List)
        {
            spawns2Active = false;
            spawns3Active = true;
        }

        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";


        //If player is in the game scene, enemies can spawn, else they cannot
        if (isInGameScene == true)
        {
            stop = false;
        }
        else
        {
            stop = true;
        }

       // Debug.Log("Stop = " + stop);
    }

    private void FixedUpdate()
    {
        //after _ sec decrease the time between new spawns
        if (timerMostWait >= timeToIncrease)
        {
            if (spawnMostWait > 4f) // Prevent negative or very small intervals
            {
                spawnMostWait -= increaseAmount;
            }

            timerMostWait = 0f;
        }
    }

    public void InitOnLoad()
    {
        //If player is in the game scene, enemies can spawn, else they cannot
        if (isInGameScene == true)
        {
            stop = false;
        }
        else
        {
            stop = true;
        }

        player = GameObject.FindWithTag("Player");
        StartCoroutine(waitSpawner());
        spawns1Active = true;
        spawns2Active = false;
        spawns3Active = false;

        //gets scene name
        Scene currentScene = SceneManager.GetActiveScene();
        isInGameScene = currentScene.name == "GameScene";


       
    }

    IEnumerator waitSpawner()
    {
        Debug.Log("Spawner function starts");
        //caluclate players position
        Vector3 playerPosition = player.transform.position;

        if(!stop)
        {
            //Debug.Log("Stop is false");
            //return null if no enemies are on screen
           /* if (spawns1.Count == 0 || spawns2.Count == 0 || spawns3.Count == 0)
            {
                yield return null;
                Debug.Log("Yield null");
               // continue;
            }*/
            
            //Spawning logic
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(spawnDistanceMin, spawnDistanceMax);
            Vector3 spawnPosition = playerPosition + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

            //Depending on what spawner list is active, set randomEnemy to the correct range
            if (spawns1Active == true)
            {
                Debug.Log("Spawns 1 active");
                randomEnemy = Random.Range(0, spawns1.Count);
                Instantiate(spawns1[randomEnemy], spawnPosition, player.transform.rotation);
            }
            else if(spawns2Active == true)
            {
                Debug.Log("Spawns 2 active");
                randomEnemy = Random.Range(0, spawns2.Count);
                Instantiate(spawns2[randomEnemy], spawnPosition, player.transform.rotation);
            }
            else if(spawns3Active == true)
            {
                Debug.Log("Spawns 3 active");
                randomEnemy = Random.Range(0, spawns3.Count);
                Instantiate(spawns3[randomEnemy], spawnPosition, player.transform.rotation);
            }
            

        }
        else
        {
            Debug.Log("Exit while loop no more spawning");
        }
        yield return new WaitForSeconds(spawnWait);
        StartCoroutine(waitSpawner());

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