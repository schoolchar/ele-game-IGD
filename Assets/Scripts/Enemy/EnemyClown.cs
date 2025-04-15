using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClown : MonoBehaviour
{
    [Header("Movement")]
    public Transform player;
    public float moveSpeed;

    [Header("Stopping Distance")]
    private float stoppingDistance;

    [Header("Shoot")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    private float waitTime = 4f;

    void Start()
    {
        StartCoroutine(TimeShoot());
        stoppingDistance = 5f;
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
            moveSpeed = 0f;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, spawnPt.transform.position, spawnPt.transform.rotation);
        StartCoroutine(TimeShoot());
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
