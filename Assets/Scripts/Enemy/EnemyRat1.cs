using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRat1 : MonoBehaviour
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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Pause = GameObject.Find("GameManager").GetComponent<Pause>();
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

        //If enemy is within stopping distance, enemy stops moving, else the enemy activily follows player.
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
}
