using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClown : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;
    public Animator animator;
    public bool isStopped;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    void Start()
    {
        animator.SetBool("IsMoving", true);
        stoppingDistance = 10f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        animator = GetComponentInChildren<Animator>();
        isStopped = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);

        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            animator.SetBool("IsMoving", true);
            isStopped = false;
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 0f;
            isStopped = true;
            animator.SetBool("IsMoving", false);
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage();

        if (_other.gameObject.tag == "Projectile")
        {
            Destroy(_other.gameObject);
        }
    }
}
