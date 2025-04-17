using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClown : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;
    public Animator animator;
    private bool isStopped;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    [Header("Shoot")]
    //[SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    //private float waitTime = 2.5f;
    public ObjectPooler pooler;

    void Start()
    {
        animator.SetBool("IsMoving", true);
        //isStopped = false;
        //StartCoroutine(TimeShoot());
        stoppingDistance = 10f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);
        
        // Set Active bullet
        GameObject bullet = pooler.GetObject();
        if (bullet != null)
        {
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
        }

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
