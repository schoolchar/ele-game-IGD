using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClown : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;
    public Animator animator;
    //private bool isStopped;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    [Header("Shoot")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    private float waitTime = 2.5f;

    Pause Pause;
    public AudioSource clownSound;

    void Start()
    {
        clownSound = GetComponent<AudioSource>();
        animator.SetBool("IsMoving", true);
        //isStopped = false;
        StartCoroutine(TimeShoot());
        stoppingDistance = 10f;
        moveSpeed = 5f;
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        animator = GetComponentInChildren<Animator>();
        Pause = GameObject.Find("GameManager").GetComponent<Pause>();
    }

    void Update()
    {
        if (Pause.PauseMenu.activeSelf)
        {
            Debug.Log("Rat sound not Playing");
            clownSound.Pause();
        }
        else
        {
            Debug.Log("Rat sound Playing");
            clownSound.UnPause();
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);

        //If enemy is within stopping distance, the enemy stops moving, else the enemy actilvily follows player.
        if (distance > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position);
            animator.SetBool("IsMoving", true);
            //isStopped = false;
            moveSpeed = 5f;
        }
        else
        {
            moveSpeed = 0f;
            //isStopped = true;
            animator.SetBool("IsMoving", false);
        }
    }

    void Shoot()
    {
        //if (isStopped == true)
        //{
            Instantiate(bullet, spawnPt.transform.position, spawnPt.transform.rotation);
            StartCoroutine(TimeShoot());
        //}
    }

    IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(waitTime);
        Shoot();
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
