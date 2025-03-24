using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
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
        HitPlayer(collision);
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }


    void HitPlayer(Collision _collision)
    {
        if (_collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy hit");
            
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
