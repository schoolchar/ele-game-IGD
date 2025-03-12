using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;

    void Start()
    {
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        transform.LookAt(player);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        
    }
}
