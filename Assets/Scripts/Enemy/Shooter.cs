using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ObjectPooler pooler;
    //public GameObject EnemyClownObj;
    public Transform player;
    EnemyClown enemyClown;
    private float shootTimer;

    private void Start()
    {
        enemyClown = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyClown>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (enemyClown.isStopped == true)
        {
            Debug.Log("Attack!!");
        if (shootTimer > 2.5f)
            {
                GameObject bullet = pooler.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                }

                shootTimer = 0;
            }
        }
       
    }
}
