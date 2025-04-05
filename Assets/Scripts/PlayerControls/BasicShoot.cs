using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicShoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject spawnPt;
    [SerializeField] private GameObject playerPhys;
   // [SerializeField] GameObject fire;
    private int waitTime = 3;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        StartCoroutine(TimeShoot());
       // Debug.Log("Ring of fire");
       // Instantiate(fire, player.transform.position, player.transform.rotation);
    }


    void Shoot()
    {
        Instantiate(bullet, spawnPt.transform.position, playerPhys.transform.rotation);
        StartCoroutine(TimeShoot());
    }


    IEnumerator TimeShoot()
    {
        yield return new WaitForSeconds(waitTime);
        Shoot();
    }
}
