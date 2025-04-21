using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public ObjectPooler pooler;
    public Transform player;
    EnemyClown enemyClown;
    private float shootTimer;

    private void Start()
    {
        //find clown
        enemyClown = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyClown>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        //If clown is not moving, set active bullet
        if (enemyClown.isStopped == true)
        {
            //After 2.8 seconds have passed since the last bullet, set active the next
            if (shootTimer > 2.6f)
            {
                GameObject bullet = pooler.GetObject();
                if (bullet != null)
                {
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = transform.rotation;
                }

                //reset shoot timer
                shootTimer = 0;
            }
        }
       
    }
}
