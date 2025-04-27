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
        //find player
        player = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
    }

    void OnEnable()
    {
        timer = lifeTime;
    }

    void Update()
    {
        //always look at the player
        transform.LookAt(player.transform.position);
        //movement
        transform.Translate(transform.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;

        //when timer runs out, set bullet inactive
        if (timer <= 0)
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        ObjectPooler _op = FindObjectOfType<ObjectPooler>();
        if (_op != null)
            _op.ReturnObject(gameObject);
        else
            Destroy(this.gameObject);
    }

    //when bullet collides with player, set bullet inactive
    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.tag == "Player")
        {
            ReturnToPool();
        }
    }
}
