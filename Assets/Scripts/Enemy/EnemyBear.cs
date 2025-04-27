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
    PlayerHealth playerHealth;
    AudioSource bearSound;
    private float backUpTimer;
    public bool backUp;

    private Vector3 backUpDirection;

    // Start is called before the first frame update
    void Start()
    {
        bearSound = GetComponent<AudioSource>();
        baseSpeed = 8f;
        randomSpeed = 5f;
        timerDuration = Random.Range(4f, 10f);
        increaseSpeed = 15f;
        stoppingDistance = 2.3f;

        moveSpeed = baseSpeed + Random.Range(-randomSpeed, randomSpeed);
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();  
    }

    // Update is called once per frame
    void Update()
    {
        //If time is set to 0, pause sound
        if (Time.timeScale == 0f)
        {
            Debug.Log("Bear sound not Playing");
            bearSound.Pause();
        }
        else
        {
            Debug.Log("Bear sound Playing");
            bearSound.UnPause();
        }

        //If enemy is within stopping distance, enemy stops moving, else the enemy activily follows player.
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

        if (backUp == true)
        {
            backUpTimer += Time.deltaTime;
            if (backUpTimer < 4f)
            {
                transform.position += backUpDirection * moveSpeed * Time.deltaTime;
            }
            else
            {
                backUp = false;
                backUpTimer = 0f;
            }
            return;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            moveSpeed = 5f;
        }

        //If current time is greater than or equil to what is left of the timer duration, increase movment speed
        currentTime += Time.deltaTime;
        if (currentTime >= timerDuration)
        {
            moveSpeed += increaseSpeed;
            currentTime = 0f;
        }

        //when player is dead, all enemies are destroyed
        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerBody"))
        {
            backUp = true;
            backUpTimer = 0f;

            backUpDirection = (transform.position - player.position).normalized;
        }
    }

}
