using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClownPie : MonoBehaviour
{
    //[SerializeField] GameObject bullet;
    public float speed = 10f;
    public float lifeTime = 20f;

    private float timer;

    void OnEnable()
    {
        timer = lifeTime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
}
