using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRat2 : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    public bool playerAlive;

    void Start()
    {
        stoppingDistance = 1.1f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 0;
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
