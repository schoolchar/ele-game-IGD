using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public int speed = 5;

    private void Start()
    {
        StartCoroutine(BulletLifetime());
    }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        HitEnemy(collision);
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
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1); //1 is placeholder damage
            //Destroy(_collision.gameObject);
            DestroyBullet();
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

