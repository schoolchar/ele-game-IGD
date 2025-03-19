using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Knifethrow : MonoBehaviour
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

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            GameObject nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null && hasKnife == true)
            {
                ShootAt(nearestEnemy);
                nextFireTime = Time.time + 1.0f / fireRate;
            }
        }
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

        return nearestEnemy;
    }

    void ShootAt(GameObject target)
    {
       // GameObject projectile = Instantiate(knife, transform.position + target.transform.position.normalized, Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject projectile = Instantiate(knife, transform.position, Quaternion.identity);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
  
        if (rb != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }
}