using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat2 : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    private float moveSpeed;

    [Header("Stopping Distance")]
    private float stoppingDistance;
    public bool playerAlive;
    PlayerHealth playerHealth;
    public AudioSource ratSound;
    private float backUpTimer;
    public bool backUp;
    private Vector3 backUpDirection;

    void Start()
    {
        ratSound = GetComponent<AudioSource>();
        stoppingDistance = 1.1f;
        moveSpeed = 3f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        //If time is set to 0, pause sound
        if (Time.timeScale == 0f)
        {
            Debug.Log("Rat sound not Playing");
            ratSound.Pause();
        }
        else
        {
            Debug.Log("Rat sound Playing");
            ratSound.UnPause();
        }

        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);

        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            moveSpeed = 3f;
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

        //when player is dead, all enemies are destroyed
        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    
    private void OnCollisionEnter(Collision other)
    { 
        //If rat has entered the ratgaze collider, movement speed is 1
        if (other.gameObject.CompareTag("RatGaze"))
        {
            moveSpeed = 1f;
            Debug.Log("RatGazeOn");
        }

        if (other.gameObject.CompareTag("PlayerBody"))
        {
            backUp = true;
            backUpTimer = 0f;

            backUpDirection = (transform.position - player.position).normalized;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //If rat has entered the ratgaze collider, movement speed is 5
        if (other.gameObject.CompareTag("RatGaze"))
        {
            moveSpeed = 5f;
            Debug.Log("RatGazeOn");
        }
    }
}
