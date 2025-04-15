using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    private int waitTime = 3;
    public float moveSpeed = 10f;

    private void Start()
    {
        StartCoroutine(TimeShoot());
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        Destroy(bullet, 5f);
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

    private void OnCollisionEnter(Collision collision)
    {
        HitPlayer(collision);
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
}
