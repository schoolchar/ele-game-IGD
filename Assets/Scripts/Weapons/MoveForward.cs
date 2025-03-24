using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int speed = 5;
    public float enemyTakeDamage;
    public GameObject bullet;

    private GameObject enemy;
    EnemyHealth enemyHealth;

    private void Start()
    {
        StartCoroutine(BulletLifetime());
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        enemyHealth.GetComponent<EnemyHealth>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        HitEnemy(collision);
        DestroyBullet();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void HitEnemy(Collision _collision)
    {
        if(_collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            enemyHealth.TakeDamage();
        }
        if (_collision.gameObject.tag == "Projectile")
        {
            Debug.Log("Enemy hit");
            Destroy(_collision.gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }

    IEnumerator BulletLifetime()
    {
        yield return new WaitForSeconds(5);
        DestroyBullet();
    }
}

