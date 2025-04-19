using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBear : MonoBehaviour
{
    public Transform player;

    [Header("Inital Random Speed")]
    public float moveSpeed;
    public float baseSpeed;
    public float randomSpeed;

    [Header("Speed Increase After _ Sec")]
    public float timerDuration;
    private float currentTime;
    public float increaseSpeed;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    Pause Pause;
    PlayerHealth playerHealth;
    public AudioSource bearSound;


    // Start is called before the first frame update
    void Start()
    {
        bearSound = GetComponent<AudioSource>();
        baseSpeed = 8f;
        randomSpeed = 5f;
        timerDuration = Random.Range(4f, 10f);
        increaseSpeed = 15f;
        stoppingDistance = 3f;

        moveSpeed = baseSpeed + Random.Range(-randomSpeed, randomSpeed);
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Pause = GameObject.Find("GameManager").GetComponent<Pause>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Pause.PauseMenu.activeSelf)
        {
            Debug.Log("Rat sound not Playing");
            bearSound.Pause();
        }
        else
        {
            Debug.Log("Rat sound Playing");
            bearSound.UnPause();
        }
        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            moveSpeed = baseSpeed + Random.Range(-randomSpeed, randomSpeed);
        }
        else
        {
            moveSpeed = 0; 
        } 

        //If current time is greater than or equil to what is left of the timer duration, increase movment speed
        currentTime += Time.deltaTime;
        if (currentTime >= timerDuration)
        {
            moveSpeed += increaseSpeed;
            currentTime = 0f;
        }


        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

}
