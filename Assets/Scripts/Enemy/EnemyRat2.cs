using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat2 : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;

    [Header("Stopping Distance")]
    private float stoppingDistance;
    public bool playerAlive;
    Pause Pause;
    PlayerHealth playerHealth;
    AudioSource ratSound;

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
        
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);

        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 0;
        }


        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision _other)
    {
        moveSpeed = 1f;
        Debug.Log("RatGazeOn");
    }

    private void OnCollisionExit(Collision _other)
    {
        moveSpeed = 5f;
        Debug.Log("RatGazeOff");
    }
}
