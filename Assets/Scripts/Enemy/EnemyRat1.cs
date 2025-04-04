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


    void Start()
    {
        stoppingDistance = 1.1f;
        moveSpeed = 4f;
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
            moveSpeed = 4f;
        }
        else
        {
            moveSpeed = 0;
        }

    }
}
