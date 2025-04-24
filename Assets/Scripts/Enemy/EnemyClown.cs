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

    [Header("Shoot")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    private float waitTime = 2.5f;
    PlayerHealth playerHealth;
    AudioSource clownSound;

    void Start()
    {
        clownSound = GetComponent<AudioSource>();
        animator.SetBool("IsMoving", true);
        stoppingDistance = 10f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        animator = GetComponentInChildren<Animator>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        isStopped = false;
    }

    void Update()
    {
        //If time is set to 0, pause sound
        if (Time.timeScale == 0f)
        {
            Debug.Log("clown sound not Playing");
            clownSound.Pause();
        }
        else
        {
            Debug.Log("clown sound Playing");
            clownSound.UnPause();
        }
        
        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        float distance = Vector3.Distance(transform.position, player.transform.position);
        
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

        //when player is dead, all enemies are destroyed
        if (playerHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /*private void OnCollisionEnter(Collision _other)
    {
        //TakeDamage();

        if (_other.gameObject.tag == "Projectile")
        {
            Destroy(_other.gameObject);
        }
    }*/
}
