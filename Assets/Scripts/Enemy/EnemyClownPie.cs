using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
{
    //[SerializeField] GameObject bullet;
    public float speed = 10f;
    public float lifeTime = 15f;
    public Transform player;

    private float timer;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }

    void OnEnable()
    {
        timer = lifeTime;
    }

    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
        transform.Translate(transform.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        FindObjectOfType<ObjectPooler>().ReturnObject(gameObject);
    }

    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.tag == "Player")
        {
            ReturnToPool();
        }
    }
}
