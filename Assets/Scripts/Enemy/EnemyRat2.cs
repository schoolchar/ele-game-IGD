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

    Pause Pause;
    PlayerHealth playerHealth;
    public AudioSource ratSound;

    void Start()
    {
        ratSound = GetComponent<AudioSource>();
        stoppingDistance = 1.1f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        Pause = GameObject.Find("GameManager").GetComponent<Pause>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        //If pause menu is active , pause sound
        if (Pause.PauseMenu.activeSelf)
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
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 0;
        }

        //when player is dead, all enemies are destroyed
        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    //If rat has entered the ratgaze collider, movement speed is 1
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RatGaze")
        {
            moveSpeed = 1f;
            Debug.Log("RatGazeOn");
        }
        
    }

    //If rat has exited the ratgaze collider, movement speed is back to 5
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RatGaze")
        {
            moveSpeed = 5f;
            Debug.Log("RatGazeOn");
        }
    }
}
