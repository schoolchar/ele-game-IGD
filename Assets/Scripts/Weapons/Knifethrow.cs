using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Knifethrow : WeaponBase
{
    // The layer mask for the enemy
    public LayerMask EnemyLayerMask;

    // The prefab of the sphere to shoot
    public GameObject knife;

    // The speed of the projectile
    public float projectileSpeed = 20.0f;

    // The rate of fire
    public float fireRate = 1.0f;

    private float nextFireTime = 0.0f;

    //get component player script
    public bool hasKnife = true;

    bool knifeActive;


    //Player
    [SerializeField] private GameObject player;

    public override void ActivateThisWeapon()
    {
        Debug.Log("Knife activated");
        if (!hasKnife)
        {
            hasKnife = true;
            StartCoroutine(TimeShooting());
            knifeActive = true;
        }
        else
        {
            projectileSpeed += 1.5f;
        }
        
    }

    void Update()
    {
        /*if (Time.time > nextFireTime)
        {
            Debug.Log("time greter than fire time");
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && hasKnife == true)
            {
                ShootAt(nearestEnemy);
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoDamage(collision);
    }

    GameObject FindNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, EnemyLayerMask);
        foreach (Collider hitCollider in hitColliders)
        {
            float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = hitCollider.gameObject;
            }
        }
        Debug.Log("Nearest enemy = " + nearestEnemy);
        return nearestEnemy;
    }

    void ShootAt(GameObject target)
    {
        Debug.Log("Knife throw called");
        GameObject projectile = Instantiate(knife, transform.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
  
        if (rb != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    void DoDamage(Collision _collision)
    {
        if (_collision.gameObject.layer == 8)
        {
            _collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
            transform.position = player.transform.position;
        }
    }

    IEnumerator TimeShooting()
    {
        yield return new WaitForSeconds(fireRate);
        Debug.Log("Knife");
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null && hasKnife == true)
        {
            Debug.Log("Conditions for knife throw met");
            ShootAt(nearestEnemy);
            nextFireTime = Time.time + 1.0f / fireRate;
        }
        StartCoroutine(TimeShooting());
    }
}