using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
   // [SerializeField] GameObject fire;
    private int waitTime = 3;
    private void Start()
    {
        StartCoroutine(TimeShoot());
       // Debug.Log("Ring of fire");
       // Instantiate(fire, player.transform.position, player.transform.rotation);
    }

    private void Update()
    {

    }
    void Shoot()
    {
       // Instantiate(bullet, spawnPt.transform.position, spawnPt.transform.rotation);
        StartCoroutine(TimeShoot());
    }


    IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(waitTime);
       // Shoot();
    }
}
