using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public float enemyTakeDamage =1;
    public GameObject bullet;

    private GameObject enemy;
    EnemyHealth enemyHealth;
    GameObject player;
    Rigidbody rb;

    private void Start()
    {
       rb = GetComponent<Rigidbody>();
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
        StartCoroutine(BulletLifetime());
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
        //DestroyBullet();
    }

    private void Move()
    {
        rb.MovePosition(transform.forward * Time.deltaTime);
        //transform.Translate(transform.forward * speed * Time.deltaTime);
    }


    void HitEnemy(Collision _collision)
    {
        if(_collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            enemyHealth = _collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(enemyTakeDamage);
            //Destroy(_collision.gameObject);

            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Debug.Log("Bullet destroy called");
        Destroy(this.gameObject);
    }

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(5);
        DestroyBullet();
    }
}

