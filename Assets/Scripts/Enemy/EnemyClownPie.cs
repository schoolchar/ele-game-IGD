using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    public Transform player;
    public float moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        transform.LookAt(player.transform.position);
        Destroy(bullet, 10f);
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
