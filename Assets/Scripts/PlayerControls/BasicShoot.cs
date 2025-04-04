using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    private int waitTime = 3;


    private void Start()
    {
        StartCoroutine(TimeShoot());
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
}
