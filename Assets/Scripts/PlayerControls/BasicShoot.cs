using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class BasicShoot : MonoBehaviour
{
    public Playercollisions Playercollisions;
    public Transform player;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject fire;
    private int waitTime = 3;
    private void Start()
    {
        StartCoroutine(TimeShoot());
       // Debug.Log("Ring of fire");
        Instantiate(fire, player.transform.position, player.transform.rotation);
    }

    private void Update()
    {
       // Ringoffire();
    }
    void Shoot()
    {
       // Instantiate(bullet, spawnPt.transform.position, spawnPt.transform.rotation);
        StartCoroutine(TimeShoot());
    }


    IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(waitTime);

        ShootKnife();
       // Shoot();
    }

    void ShootKnife()
    {
        /*if(Playercollisions.Playerhasknife == true)
        {
            Debug.Log("Knife thrown");
            Instantiate(knife, spawnPt.transform.position, spawnPt.transform.rotation);
        }*/

        Instantiate(knife, spawnPt.transform.position, spawnPt.transform.rotation);
        StartCoroutine(TimeShoot());
    }

    void Ringoffire()
    {
        if(Playercollisions.Playerhasfire == true && Playercollisions.firespawned == true)
        {
            Debug.Log("Ring of fire");
            Instantiate(fire, player.transform.position, player.transform.rotation);
            Playercollisions.firespawned = false;
        }
    }
}
